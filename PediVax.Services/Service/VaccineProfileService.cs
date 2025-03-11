using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PediVax.BusinessObjects.DTO.VaccineProfileDTO;
using PediVax.BusinessObjects.DTO.VaccineScheduleDTO;
using PediVax.BusinessObjects.Enum;
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

        public VaccineProfileService(IVaccineProfileRepository vaccineProfileRepository, IMapper mapper, ILogger<VaccineProfile> logger, IHttpContextAccessor httpContextAccessor, IDiseaseRepository diseaseRepository, IVaccineScheduleRepository vaccineScheduleRepository, IChildProfileRepository childProfileRepository)
        {
            _vaccineProfileRepository = vaccineProfileRepository;
            _mapper = mapper;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _diseaseRepository = diseaseRepository;
            _vaccineScheduleRepository = vaccineScheduleRepository;
            _childProfileRepository = childProfileRepository;
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

        //public async Task<VaccineProfileResponseDTO> AddVaccineProfile(CreateVaccineProfileDTO vaccineProfileDTO, CancellationToken cancellationToken)
        //{
        //    if (vaccineProfileDTO == null)
        //    {
        //        throw new ArgumentNullException(nameof(CreateVaccineProfileDTO), "Vaccine schedule data is required");
        //    }
        //    try
        //    {
        //        var disease = await _diseaseRepository.GetDiseaseById(vaccineProfileDTO.DiseaseId, cancellationToken);
        //        if (disease == null || disease.IsActive != EnumList.IsActive.Active)
        //        {
        //            throw new ArgumentException("Invalid or inactive DiseaseId");
        //        }

        //        var vaccineProfile = _mapper.Map<VaccineProfile>(vaccineProfileDTO);

        //        vaccineProfile.CreatedBy = GetCurrentUserName();
        //        vaccineProfile.CreatedDate = DateTime.UtcNow;
        //        vaccineProfile.VaccinationDate = vaccineProfileDTO.VaccinationDate;
        //        vaccineProfile.IsCompleted = vaccineProfileDTO.IsCompleted;
        //        vaccineProfile.IsActive = EnumList.IsActive.Active;
        //        SetAuditFields(vaccineProfile);

        //        if (await _vaccineProfileRepository.AddVaccineProfile(vaccineProfile, cancellationToken) <= 0)
        //        {
        //            throw new ApplicationException("Adding new vaccine profile failed");
        //        }

        //        return _mapper.Map<VaccineProfileResponseDTO>(vaccineProfile);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error adding new vaccine profile");
        //        throw new ApplicationException("Error while saving vaccine profile", ex);
        //    }
        //}

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

            var vaccineProfiles = new List<VaccineProfile>();

            foreach (var schedule in vaccineSchedules)
            {
                DateTime scheduledDate = birthDate.AddMonths(schedule.AgeInMonths);

                var vaccineProfile = new VaccineProfile
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

                vaccineProfiles.Add(vaccineProfile);
            }

            await _vaccineProfileRepository.GenerateVaccineProfile(vaccineProfiles, cancellationToken);
            return _mapper.Map<List<VaccineProfileResponseDTO>>(vaccineProfiles);
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

                SetAuditFields(vaccineProfile);
                vaccineProfile.VaccinationDate = updateVaccineProfileDTO.VaccinationDate ?? vaccineProfile.VaccinationDate;
                vaccineProfile.IsCompleted = updateVaccineProfileDTO.IsCompleted ?? vaccineProfile.IsCompleted;
                vaccineProfile.ChildId = updateVaccineProfileDTO.ChildId;
                vaccineProfile.DiseaseId = updateVaccineProfileDTO.DiseaseId;

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