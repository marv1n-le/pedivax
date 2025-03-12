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
using PediVax.Services.IService;

namespace PediVax.Services.Service;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<AppointmentService> _logger;

    public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<AppointmentService> logger)
    {
        _appointmentRepository = appointmentRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
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

        // ✅ Kiểm tra nếu cả VaccinePackageId và VaccineId cùng tồn tại
        if (createAppointmentDTO.VaccinePackageId.HasValue && createAppointmentDTO.VaccineId.HasValue)
        {
            throw new ArgumentException("Bạn không thể chọn cả Vaccine Package và Vaccine riêng lẻ cùng lúc. Vui lòng chọn một trong hai.");
        }

        try
        {
            var appointment = _mapper.Map<Appointment>(createAppointmentDTO);
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            appointment.CreatedBy = GetCurrentUserName();
            appointment.CreatedDate = DateTime.UtcNow;
            appointment.AppointmentStatus = EnumList.AppointmentStatus.Pending;
            SetAuditFields(appointment);

            // ✅ Chỉ kiểm tra số lượng nếu chọn VaccinePackage
            if (appointment.VaccinePackageId.HasValue)
            {
                var count = await _appointmentRepository.GetQuantityAppointmentByPackageIdAndVaccineId(
                    appointment.ChildId, (int)appointment.VaccinePackageId, (int)appointment.VaccineId, cancellationToken);

                var quantity = await _appointmentRepository.GetCountOfPackageDetail(
                    (int)appointment.VaccinePackageId, (int)appointment.VaccineId, cancellationToken);

                if (count >= quantity)
                {
                    throw new InvalidOperationException("Số lượng cuộc hẹn đã đạt giới hạn của gói vắc xin.");
                }
            }

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

            SetAuditFields(appointment);
            appointment.PaymentId = updateAppointmentDTO.PaymentId;
            appointment.ChildId = updateAppointmentDTO.ChildId;
            appointment.VaccineId = updateAppointmentDTO.VaccineId;
            appointment.VaccinePackageId = updateAppointmentDTO.VaccinePackageId;
            appointment.AppointmentDate = updateAppointmentDTO.AppointmentDate;
            appointment.AppointmentStatus = updateAppointmentDTO.AppointmentStatus;
            appointment.IsActive = updateAppointmentDTO.IsActive;

            int rowAffected = await _appointmentRepository.UpdateAppointment(appointment, cancellationToken);
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

    public async Task<bool> UpdateAppointmentStatus(int appointmentId, EnumList.AppointmentStatus appointmentStatus, CancellationToken cancellationToken)
    {
        var appointment = await _appointmentRepository.GetAppointmentById(appointmentId, cancellationToken);
        if ((int)appointment.AppointmentStatus == 1)
        {
            if ((int)appointmentStatus == 2 || (int)appointmentStatus == 5)
            {
                appointment.AppointmentStatus = appointmentStatus;
            }
            else
            {
                throw new ArgumentException("AppoitmentId is Pending, You can only change to WaitingForInjection (2) or Canceled (5).");
            }
        }
        else if ((int)appointment.AppointmentStatus == 2)
        {
            if ((int)appointmentStatus == 3)
            {
                appointment.AppointmentStatus = appointmentStatus;
            }
            else
            {
                throw new ArgumentException("AppoitmentId is WaitingForInjection, You can only change to WaitingForResponse (3).");
            }
        }
        else if ((int)appointment.AppointmentStatus == 3) 
        {
            if ((int)appointmentStatus == 4)
            {
                appointment.AppointmentStatus = appointmentStatus;
            }
            else
            {
                throw new ArgumentException("AppoitmentId is WaitingForResponse, You can only change to Completed (4).");
            }
        }
        else
        {
            throw new ArgumentException("You can not change Appoitment Status");
        }
        var rowAffected = await _appointmentRepository.UpdateAppointment(appointment, cancellationToken);
        return rowAffected > 0;

    }
}