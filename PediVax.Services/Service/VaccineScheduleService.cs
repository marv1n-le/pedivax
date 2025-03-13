using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PediVax.BusinessObjects.DTO.VaccineScheduleDTO;
using PediVax.BusinessObjects.Enum;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace PediVax.Services.Service
{
    public class VaccineScheduleService : IVaccineScheduleService
    {
        private readonly IVaccineScheduleRepository _vaccineScheduleRepository;
        private readonly IVaccineRepository _vaccineRepository;
        private readonly IDiseaseRepository _diseaseRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<VaccineScheduleService> _logger;

        public VaccineScheduleService(
            IVaccineScheduleRepository vaccineScheduleRepository,
            IVaccineRepository vaccineRepository,
            IDiseaseRepository diseaseRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ILogger<VaccineScheduleService> logger)
        {
            _vaccineScheduleRepository = vaccineScheduleRepository;
            _vaccineRepository = vaccineRepository;
            _diseaseRepository = diseaseRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        private string GetCurrentUserName()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "System";
        }

        private void SetAuditFields(VaccineSchedule vaccineSchedule)
        {
            vaccineSchedule.ModifiedBy = GetCurrentUserName();
            vaccineSchedule.ModifiedDate = DateTime.UtcNow;
        }

        public async Task<List<VaccineScheduleResponseDTO>> GetAllVaccineSchedule(CancellationToken cancellationToken)
        {
            var vaccineSchedules = await _vaccineScheduleRepository.GetAllVaccineSchedule(cancellationToken);

            if (vaccineSchedules == null || !vaccineSchedules.Any())
            {
                _logger.LogWarning("No vaccine schedules found.");
                return new List<VaccineScheduleResponseDTO>();
            }

            foreach (var schedule in vaccineSchedules)
            {
                schedule.Disease = await _diseaseRepository.GetDiseaseById(schedule.DiseaseId, cancellationToken);
            }

            return _mapper.Map<List<VaccineScheduleResponseDTO>>(vaccineSchedules);
        }

        public async Task<VaccineScheduleResponseDTO> GetVaccineScheduleById(int vaccineScheduleId, CancellationToken cancellationToken)
        {
            if (vaccineScheduleId <= 0)
            {
                _logger.LogWarning("Invalid vaccine schedule ID: {vaccineScheduleId}", vaccineScheduleId);
                throw new ArgumentException("Invalid vaccine schedule ID");
            }

            var vaccineSchedule = await _vaccineScheduleRepository.GetVaccineScheduleById(vaccineScheduleId, cancellationToken);
            if (vaccineSchedule == null)
            {
                _logger.LogWarning("Vaccine schedule not found: {vaccineScheduleId}", vaccineScheduleId);
                throw new KeyNotFoundException("Vaccine schedule not found");
            }
            vaccineSchedule.Disease = await _diseaseRepository.GetDiseaseById(vaccineSchedule.DiseaseId, cancellationToken);

            return _mapper.Map<VaccineScheduleResponseDTO>(vaccineSchedule);
        }

        public async Task<VaccineScheduleResponseDTO> CreateVaccineSchedule(CreateVaccineScheduleDTO vaccineScheduleRequestDTO, CancellationToken cancellationToken)
        {
            if (vaccineScheduleRequestDTO == null)
            {
                throw new ArgumentNullException(nameof(CreateVaccineScheduleDTO), "Vaccine schedule data is required");
            }

            try
            {
                var existingVaccineSchedule = await _vaccineScheduleRepository.GetAllVaccineSchedule(cancellationToken);
                if (existingVaccineSchedule.Any(v => v.DiseaseId == vaccineScheduleRequestDTO.DiseaseId && v.AgeInMonths == vaccineScheduleRequestDTO.AgeInMonths && v.DoseNumber == vaccineScheduleRequestDTO.DoseNumber))
                {
                    throw new ApplicationException($"Vaccine schedule with DiseaseId {vaccineScheduleRequestDTO.DiseaseId}, AgeInMonths {vaccineScheduleRequestDTO.AgeInMonths}, and DoseNumber {vaccineScheduleRequestDTO.DoseNumber} already exists.");
                }

                var disease = await _diseaseRepository.GetDiseaseById(vaccineScheduleRequestDTO.DiseaseId, cancellationToken);
                if (disease == null || disease.IsActive > EnumList.IsActive.Active)
                {
                    throw new ArgumentException("Invalid or inactive DiseaseId");
                }

                var vaccineSchedule = _mapper.Map<VaccineSchedule>(vaccineScheduleRequestDTO);
                vaccineSchedule.CreatedBy = GetCurrentUserName();
                vaccineSchedule.CreatedDate = DateTime.UtcNow;
                vaccineSchedule.IsActive = EnumList.IsActive.Active;
                SetAuditFields(vaccineSchedule);

                if (await _vaccineScheduleRepository.AddVaccineSchedule(vaccineSchedule, cancellationToken) <= 0)
                    throw new ApplicationException("Adding new vaccine schedule failed");

                vaccineSchedule.Disease = disease;

                return _mapper.Map<VaccineScheduleResponseDTO>(vaccineSchedule);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding new vaccine schedule");
                throw new ApplicationException("Error while saving vaccine schedule", ex);
            }
        }

        public async Task<bool> UpdateVaccineSchedule(int vaccineScheduleId, UpdateVaccineScheduleDTO updateVaccineScheduleDTO, CancellationToken cancellationToken)
        {
            if (vaccineScheduleId <= 0)
            {
                _logger.LogWarning("Invalid vaccine schedule ID: {vaccineScheduleId}", vaccineScheduleId);
                throw new ArgumentException("Invalid vaccine schedule ID");
            }

            try
            {
                var vaccineSchedule = await _vaccineScheduleRepository.GetVaccineScheduleById(vaccineScheduleId, cancellationToken);
                if (vaccineSchedule == null)
                {
                    _logger.LogWarning("Vaccine schedule not found for ID: {vaccineScheduleId}", vaccineScheduleId);
                    throw new KeyNotFoundException("Vaccine schedule not found");
                }

                SetAuditFields(vaccineSchedule);
                vaccineSchedule.DiseaseId = updateVaccineScheduleDTO.DiseaseId ?? vaccineSchedule.DiseaseId;
                vaccineSchedule.AgeInMonths = updateVaccineScheduleDTO.AgeInMonths ?? vaccineSchedule.AgeInMonths;
                vaccineSchedule.DoseNumber = updateVaccineScheduleDTO.DoseNumber ?? vaccineSchedule.DoseNumber;
                var existingVaccineSchedule = await _vaccineScheduleRepository.GetAllVaccineSchedule(cancellationToken);
                if (existingVaccineSchedule.Any(v => v.DiseaseId == updateVaccineScheduleDTO.DiseaseId && v.AgeInMonths == updateVaccineScheduleDTO.AgeInMonths && v.DoseNumber == updateVaccineScheduleDTO.DoseNumber))
                {
                    throw new ApplicationException($"Vaccine schedule with DiseaseId {updateVaccineScheduleDTO.DiseaseId}, AgeInMonths {updateVaccineScheduleDTO.AgeInMonths}, and DoseNumber {updateVaccineScheduleDTO.DoseNumber} already exists.");
                }
                return await _vaccineScheduleRepository.UpdateVaccineSchedule(vaccineSchedule, cancellationToken) > 0;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating vaccine schedule with ID {vaccineScheduleId}", vaccineScheduleId);
                throw new ApplicationException("Error while updating vaccine schedule", ex);
            }
        }

        public async Task<bool> DeleteVaccineSchedule(int vaccineScheduleId, CancellationToken cancellationToken)
        {
            if (vaccineScheduleId <= 0)
            {
                _logger.LogWarning("Invalid vaccine schedule ID: {vaccineScheduleId}", vaccineScheduleId);
                throw new ArgumentException("Invalid vaccine schedule ID");
            }

            return await _vaccineScheduleRepository.DeleteVaccineSchedule(vaccineScheduleId, cancellationToken);
        }
    }
}
