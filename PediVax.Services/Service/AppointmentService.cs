using System.Security.Claims;
using System.Threading;
using System.Transactions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PediVax.BusinessObjects.DTO.AppointmentDTO;
using PediVax.BusinessObjects.Enum;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Repositories.Repository;
using PediVax.Services.IService;

namespace PediVax.Services.Service;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IChildProfileRepository _childProfileRepository;
    private readonly IVaccinePackageRepository _vaccinePackageRepository;
    private readonly IVaccineRepository _vaccineRepository;
    private readonly IPaymentDetailRepository _paymentDetailRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IVaccineDiseaseRepository _vaccineDiseaseRepository;
    private readonly IVaccineProfileRepository _vaccineProfileRepository;
    private readonly ILogger<AppointmentService> _logger;

    public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<AppointmentService> logger, IChildProfileRepository childProfileRepository, IVaccinePackageRepository vaccinePackageRepository, IPaymentRepository paymentRepository, IVaccineRepository vaccineRepository, IVaccineDiseaseRepository vaccineDiseaseRepository, IVaccineProfileRepository vaccineProfileRepository, IPaymentDetailRepository paymentDetailRepository)
    {
        _appointmentRepository = appointmentRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _childProfileRepository = childProfileRepository;
        _vaccinePackageRepository = vaccinePackageRepository;
        _paymentRepository = paymentRepository;
        _vaccineRepository = vaccineRepository;
        _vaccineDiseaseRepository = vaccineDiseaseRepository;
        _vaccineProfileRepository = vaccineProfileRepository;
        _paymentDetailRepository = paymentDetailRepository; 
    }

    private string GetCurrentUserName()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "System";
    }

    private void SetAuditFields(Appointment appointment)
    {
        appointment.ModifiedBy = GetCurrentUserName();
        appointment.ModifiedDate = DateTime.UtcNow;
    }

    public async Task<List<AppointmentResponseDTO>> GetAllAppointments(CancellationToken cancellationToken)
    {
        var appointments = await _appointmentRepository.GetAllAppointments(cancellationToken);
        return _mapper.Map<List<AppointmentResponseDTO>>(appointments);
    }

    public async Task<AppointmentResponseDTO> GetAppointmentById(int appointmentId, CancellationToken cancellationToken)
    {
        if (appointmentId <= 0)
        {
            _logger.LogWarning("Invalid appointment ID: {appointmentId}", appointmentId);
            throw new ArgumentException("Invalid appointment ID");
        }

        var appointment = await _appointmentRepository.GetAppointmentById(appointmentId, cancellationToken);
        if (appointment == null)
        {
            _logger.LogWarning("Appointment with ID {appointmentId} not found", appointmentId);
            throw new KeyNotFoundException("Appointment not found");
        }

        return _mapper.Map<AppointmentResponseDTO>(appointment);
    }

    public async Task<(List<AppointmentResponseDTO> Data, int TotalCount)> GetAppointmentsPaged(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0 || pageSize <= 0)
        {
            _logger.LogWarning("Invalid pagination parameters: PageNumber={pageNumber}, PageSize={pageSize}", pageNumber, pageSize);
            throw new ArgumentException("Invalid pagination parameters");
        }

        var (appointments, totalCount) = await _appointmentRepository.GetAppointmentsPaged(pageNumber, pageSize, cancellationToken);
        return (_mapper.Map<List<AppointmentResponseDTO>>(appointments), totalCount);
    }

    public async Task<AppointmentResponseDTO> CreateAppointment(CreateAppointmentDTO createAppointmentDTO, CancellationToken cancellationToken)
    {
        if (createAppointmentDTO == null)
        {
            throw new ArgumentNullException(nameof(createAppointmentDTO), "Appointment data is required");
        }

        if (createAppointmentDTO.VaccinePackageId.HasValue && createAppointmentDTO.VaccineId.HasValue)
        {
            throw new ArgumentException("Bạn không thể chọn cả Vaccine Package và Vaccine riêng lẻ cùng lúc. Vui lòng chọn một trong hai.");
        }

        if (createAppointmentDTO.VaccineId.HasValue)
        {
            var vaccine = await _vaccineRepository.GetVaccineById(createAppointmentDTO.VaccineId.Value, cancellationToken);
            if (vaccine == null)
            {
                throw new ArgumentException("Vắc-xin không hợp lệ.");
            }
            if (vaccine.Quantity <= 0)
            {
                throw new ApplicationException("Vắc-xin này đã hết hàng. Vui lòng chọn vắc-xin khác.");
            }
        }

        var childProfile = await _childProfileRepository.GetChildProfileById(createAppointmentDTO.ChildId);
        if (childProfile == null || childProfile.UserId != createAppointmentDTO.UserId)
        {
            throw new UnauthorizedAccessException("Trẻ này không thuộc quyền sở hữu của người dùng.");
        }

        if (createAppointmentDTO.VaccinePackageId.HasValue)
        {
            var vaccinePackage = await _vaccinePackageRepository.GetVaccinePackageById(createAppointmentDTO.VaccinePackageId.Value, cancellationToken);
            if (vaccinePackage == null)
            {
                throw new ArgumentException("Gói vắc-xin không hợp lệ.");
            }

            int childAgeInMonths = ((createAppointmentDTO.AppointmentDate.Year - childProfile.DateOfBirth.Year) * 12)
                                   + createAppointmentDTO.AppointmentDate.Month - childProfile.DateOfBirth.Month;

            if (childAgeInMonths < vaccinePackage.AgeInMonths)
            {
                throw new ArgumentException($"Trẻ chưa đủ {vaccinePackage.AgeInMonths} tháng để đặt gói vắc-xin này.");
            }
        }

        try
        {
            var appointment = _mapper.Map<Appointment>(createAppointmentDTO);
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            appointment.AppointmentDate = createAppointmentDTO.AppointmentDate;
            appointment.CreatedBy = GetCurrentUserName();
            appointment.CreatedDate = DateTime.UtcNow;
            appointment.AppointmentStatus = EnumList.AppointmentStatus.Pending;
            appointment.IsActive = EnumList.IsActive.Active;
            SetAuditFields(appointment);

            if (await _appointmentRepository.AddAppointment(appointment, cancellationToken) <= 0)
            {
                throw new ApplicationException("Adding new appointment failed");
            }

            scope.Complete();
            return _mapper.Map<AppointmentResponseDTO>(appointment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding new appointment");
            throw new ApplicationException("Error while saving appointment", ex);
        }
    }

    public async Task<bool> UpdateAppointment(int id, UpdateAppointmentDTO updateAppointmentDTO, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid appointment ID");
            throw new ArgumentNullException(nameof(updateAppointmentDTO), "Appointment data is required");
        }

        try
        {
            var appointment = await _appointmentRepository.GetAppointmentById(id, cancellationToken);
            if (appointment == null)
            {
                _logger.LogWarning("Appointment with ID {id} not found", id);
                throw new KeyNotFoundException("Appointment not found");
            }

            bool isStatusChangedToSuccess = updateAppointmentDTO.AppointmentStatus.HasValue &&
                                        updateAppointmentDTO.AppointmentStatus == EnumList.AppointmentStatus.Completed &&
                                        appointment.AppointmentStatus != EnumList.AppointmentStatus.Completed;
            if (isStatusChangedToSuccess && appointment.VaccineId.HasValue)
            {
                var vaccine = await _vaccineRepository.GetVaccineById(appointment.VaccineId.Value, cancellationToken);
                if (vaccine != null)
                {
                    if (vaccine.Quantity <= 0)
                    {
                        throw new ApplicationException("Không đủ vắc-xin trong kho.");
                    }
                    vaccine.Quantity -= 1;
                    await _vaccineRepository.UpdateVaccine(vaccine, cancellationToken);
                }
            }

            SetAuditFields(appointment);
            appointment.UserId = updateAppointmentDTO.UserId ?? appointment.UserId;
            appointment.Reaction = updateAppointmentDTO.Reaction ?? appointment.Reaction;
            appointment.ChildId = updateAppointmentDTO.ChildId ?? appointment.ChildId;
            appointment.PaymentDetailId = updateAppointmentDTO.PaymentDetailId ?? appointment.PaymentDetailId;
            appointment.VaccineId = updateAppointmentDTO.VaccineId ?? appointment.VaccineId;
            appointment.VaccinePackageId = updateAppointmentDTO.VaccinePackageId ?? appointment.VaccinePackageId;
            appointment.AppointmentDate = updateAppointmentDTO.AppointmentDate ?? appointment.AppointmentDate;
            appointment.AppointmentStatus = updateAppointmentDTO.AppointmentStatus ?? appointment.AppointmentStatus;

            int rowAffected = await _appointmentRepository.UpdateAppointment(appointment, cancellationToken);

            if (isStatusChangedToSuccess)
            {
                await UpdateVaccineProfilesForCompletedAppointment(appointment, cancellationToken);
            }
            return rowAffected > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating appointment with ID {id}", id);
            throw new ApplicationException("Error while updating appointment", ex);
        }
    }

    public async Task<bool> DeleteAppointment(int appointmentId, CancellationToken cancellationToken)
    {
        if (appointmentId <= 0)
        {
            throw new ArgumentException("Invalid appointment ID");
        }

        try
        {
            return await _appointmentRepository.DeleteAppointment(appointmentId, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting appointment with ID {appointmentId}", appointmentId);
            throw new ApplicationException("Error while deleting appointment", ex);
        }
    }

    private async Task UpdateVaccineProfilesForCompletedAppointment(Appointment appointment, CancellationToken cancellationToken)
    {
        try
        {
            List<int> vaccineDiseaseIds = new();

            if (appointment.VaccineId.HasValue)
            {
                vaccineDiseaseIds = (await _vaccineDiseaseRepository.GetVaccineDiseasesByVaccineId(appointment.VaccineId.Value, cancellationToken))
                    .Select(vd => vd.DiseaseId)
                    .ToList();
            }
            else if (appointment.PaymentDetailId.HasValue)
            {
                var paymentDetail = await _paymentDetailRepository.GetPaymentDetailById(appointment.PaymentDetailId.Value, cancellationToken);
                if (paymentDetail != null)
                {
                    vaccineDiseaseIds = (await _vaccineDiseaseRepository.GetVaccineDiseasesByVaccineId(paymentDetail.VaccinePackageDetail.VaccineId, cancellationToken))
                        .Select(vd => vd.DiseaseId)
                        .ToList();
                }
            }

            if (vaccineDiseaseIds.Any())
            {
                var childProfiles = await _vaccineProfileRepository.GetVaccineProfileByChildId(appointment.ChildId, cancellationToken);

                foreach (var diseaseId in vaccineDiseaseIds)
                {
                    var profileToUpdate = childProfiles
                        .Where(vp => vp.DiseaseId == diseaseId && vp.IsCompleted == EnumList.IsCompleted.No)
                        .FirstOrDefault();

                    if (profileToUpdate != null)
                    {
                        profileToUpdate.AppointmentId = appointment.AppointmentId;
                        profileToUpdate.VaccinationDate = appointment.AppointmentDate;
                        profileToUpdate.IsCompleted = EnumList.IsCompleted.Yes;
                        profileToUpdate.ModifiedBy = appointment.ModifiedBy;
                        profileToUpdate.ModifiedDate = DateTime.UtcNow;

                        await _vaccineProfileRepository.UpdateVaccineProfile(profileToUpdate, cancellationToken);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating vaccine profiles for completed appointment {id}", appointment.AppointmentId);
            throw new ApplicationException("Error while updating appointment for vaccine profile", ex);
        }
    }


    public async Task<List<AppointmentResponseDTO>> GetAppointmentsByChildId(int childId, CancellationToken cancellationToken)
    {
        var appointments = await _appointmentRepository.GetAppointmentsByChildId(childId, cancellationToken);
        return _mapper.Map<List<AppointmentResponseDTO>>(appointments);
    }

    public async Task<List<AppointmentResponseDTO>> GetAppointmentsByDate(DateTime appointmentDate, CancellationToken cancellationToken)
    {
        var appointments = await _appointmentRepository.GetAppointmentsByDate(appointmentDate, cancellationToken);
        return _mapper.Map<List<AppointmentResponseDTO>>(appointments);
    }

    public async Task<List<AppointmentResponseDTO>> GetAppointmentsByStatus(EnumList.AppointmentStatus appointmentStatus, CancellationToken cancellationToken)
    {
        var appointments = await _appointmentRepository.GetAppointmentsByStatus(appointmentStatus, cancellationToken);
        return _mapper.Map<List<AppointmentResponseDTO>>(appointments);
    }

}