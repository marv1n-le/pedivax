using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PediVax.BusinessObjects.DTO.VaccineProfileDTO;
using PediVax.BusinessObjects.DTO.VaccineScheduleDTO;
using PediVax.BusinessObjects.Enum;
using PediVax.BusinessObjects.Helpers;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Repositories.Repository;
using PediVax.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.Services.Service
{
    public class VaccineProfileService : IVaccineProfileService
    {
        private readonly IVaccineProfileRepository _vaccineProfileRepository;
        private readonly IChildProfileRepository _childProfileRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<VaccineProfile> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IVaccineScheduleRepository _vaccineScheduleRepository;
        private readonly IDiseaseRepository _diseaseRepository;
        private readonly IAppointmentRepository _appointmentRepository;

        public VaccineProfileService(IVaccineProfileRepository vaccineProfileRepository, IMapper mapper, ILogger<VaccineProfile> logger, IHttpContextAccessor httpContextAccessor, IDiseaseRepository diseaseRepository, IVaccineScheduleRepository vaccineScheduleRepository, IChildProfileRepository childProfileRepository, IAppointmentRepository appointmentRepository)
        {
            _vaccineProfileRepository = vaccineProfileRepository;
            _mapper = mapper;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _diseaseRepository = diseaseRepository;
            _vaccineScheduleRepository = vaccineScheduleRepository;
            _childProfileRepository = childProfileRepository;
            _appointmentRepository = appointmentRepository;
        }


        private string GetCurrentUserName()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "System";
        }

        private void SetAuditFields(VaccineProfile vaccineProfile)
        {
            vaccineProfile.ModifiedBy = GetCurrentUserName();
            vaccineProfile.ModifiedDate = DateTime.UtcNow;
        }

        public async Task<List<VaccineProfileResponseDTO>> GetAllVaccineProfiles(CancellationToken cancellationToken)
        {
            var vaccineProfiles = await _vaccineProfileRepository.GetAllVaccineProfile(cancellationToken);
            return _mapper.Map<List<VaccineProfileResponseDTO>>(vaccineProfiles);
        }

        public async Task<VaccineProfileResponseDTO> GetVaccineProfileById(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid vaccine profile ID: {id}", id);
                throw new ArgumentException("Invalid vaccine profile ID");
            }

            var vaccineProfile = await _vaccineProfileRepository.GetVaccineProfileById(id, cancellationToken);
            if (vaccineProfile == null)
            {
                _logger.LogWarning("Vaccine profile not found for ID: {id}", id);
                throw new KeyNotFoundException("Vaccine profile not found");
            }

            return _mapper.Map<VaccineProfileResponseDTO>(vaccineProfile);
        }

        public async Task<List<VaccineProfileResponseDTO>> GetVaccineProfileByChildId(int childId, CancellationToken cancellationToken)
        {
            if (childId <= 0)
            {
                _logger.LogWarning("Invalid child ID: {childId}", childId);
                throw new ArgumentException("Invalid child ID");
            }

            var vaccineProfile = await _vaccineProfileRepository.GetVaccineProfileByChildId(childId, cancellationToken);
            if (vaccineProfile == null)
            {
                _logger.LogWarning("Vaccine profile not found for child ID: {childId}", childId);
                throw new KeyNotFoundException("Vaccine profile not found");
            }

            return _mapper.Map<List<VaccineProfileResponseDTO>>(vaccineProfile);
        }

        public async Task<List<VaccineProfileResponseDTO>> GenerateVaccineProfile(int childId, CancellationToken cancellationToken)
        {
            if (childId <= 0)
            {
                _logger.LogWarning("Invalid child ID: {childId}", childId);
                throw new ArgumentException("Invalid child ID");
            }

            var childProfile = await _childProfileRepository.GetChildProfileById(childId);
            if (childProfile == null)
            {
                throw new KeyNotFoundException("Child profile not found.");
            }

            DateTime birthDate = childProfile.DateOfBirth;

            var vaccineSchedules = await _vaccineScheduleRepository.GetAllVaccineSchedule(cancellationToken);

            var existingVaccineProfiles = await _vaccineProfileRepository.GetVaccineProfileByChildId(childId, cancellationToken);

            var vaccineProfilesToCreate = new List<VaccineProfile>();

            foreach (var schedule in vaccineSchedules)
            {
                DateTime scheduledDate = birthDate.AddMonths(schedule.AgeInMonths);

                var existingProfile = existingVaccineProfiles.FirstOrDefault(vp => vp.VaccineScheduleId == schedule.VaccineScheduleId);

                if (existingProfile != null)
                {
                    existingProfile.ScheduledDate = scheduledDate;
                    existingProfile.ModifiedBy = GetCurrentUserName();
                    existingProfile.ModifiedDate = DateTime.UtcNow;
                }
                else
                {
                    var newVaccineProfile = new VaccineProfile
                    {
                        VaccineScheduleId = schedule.VaccineScheduleId,
                        AppointmentId = null,
                        ChildId = childId,
                        DiseaseId = schedule.DiseaseId,
                        DoseNumber = schedule.DoseNumber,
                        VaccinationDate = null,
                        ScheduledDate = scheduledDate,
                        IsCompleted = EnumList.IsCompleted.No,
                        IsActive = EnumList.IsActive.Active,
                        CreatedBy = GetCurrentUserName(),
                        CreatedDate = DateTime.UtcNow,
                        ModifiedBy = GetCurrentUserName(),
                        ModifiedDate = DateTime.UtcNow
                    };

                    vaccineProfilesToCreate.Add(newVaccineProfile);
                }
            }
            if (vaccineProfilesToCreate.Any())
            {
                await _vaccineProfileRepository.GenerateVaccineProfile(vaccineProfilesToCreate, cancellationToken);
            }

            return _mapper.Map<List<VaccineProfileResponseDTO>>(existingVaccineProfiles.Concat(vaccineProfilesToCreate).ToList());
        }

        public async Task<bool> UpdateVaccineProfile(int id, UpdateVaccineProfileDTO updateVaccineProfileDTO, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid vaccine profile ID: {id}", id);
                throw new ArgumentException("Invalid vaccine profile ID");
            }

            try
            {
                var vaccineProfile = await _vaccineProfileRepository.GetVaccineProfileById(id, cancellationToken);
                if (vaccineProfile == null)
                {
                    _logger.LogWarning("Vaccine profile not found for ID: {id}", id);
                    throw new KeyNotFoundException("Vaccine profile not found");
                }

                if (updateVaccineProfileDTO.AppointmentId == null || updateVaccineProfileDTO.AppointmentId <= 0)
                {
                    _logger.LogWarning("AppointmentId is required for vaccine profile with ID: {id}", id);
                    throw new ArgumentException("AppointmentId is required");
                }

                var appointment = await _appointmentRepository.GetAppointmentById(updateVaccineProfileDTO.AppointmentId.Value, cancellationToken);
                if (appointment == null)
                {
                    _logger.LogWarning("Appointment not found for ID: {appointmentId}", updateVaccineProfileDTO.AppointmentId);
                    throw new KeyNotFoundException("Appointment not found");
                }

                if (appointment.AppointmentStatus != EnumList.AppointmentStatus.Completed)
                {
                    _logger.LogWarning("Appointment is not completed for ID: {appointmentId}", updateVaccineProfileDTO.AppointmentId);
                    throw new ArgumentException("Appointment is not completed");
                }

                if (appointment.ChildId != vaccineProfile.ChildId)
                {
                    _logger.LogWarning("Appointment does not belong to the child for vaccine profile with ID: {id}", id);
                    throw new ArgumentException("Appointment does not belong to the child");
                }

                SetAuditFields(vaccineProfile);

                vaccineProfile.AppointmentId = updateVaccineProfileDTO.AppointmentId;

                vaccineProfile.VaccinationDate = appointment.AppointmentDate;

                vaccineProfile.IsCompleted = EnumList.IsCompleted.Yes;


                return await _vaccineProfileRepository.UpdateVaccineProfile(vaccineProfile, cancellationToken) > 0;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating vaccine profile with ID {id}", id);
                throw new ApplicationException("Error while updating vaccine profile", ex);
            }
        }

        public async Task<bool> DeleteVaccineProfile(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid vaccine profile ID: {id}", id);
                throw new ArgumentException("Invalid vaccine profile ID");
            }

            return await _vaccineProfileRepository.DeleteVaccineProfile(id, cancellationToken);
        }
    }
}