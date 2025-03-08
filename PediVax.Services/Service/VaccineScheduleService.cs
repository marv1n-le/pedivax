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
                schedule.Vaccine = await _vaccineRepository.GetVaccineById(schedule.VaccineId, cancellationToken);
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

            // Lấy thông tin Vaccine và Disease
            vaccineSchedule.Vaccine = await _vaccineRepository.GetVaccineById(vaccineSchedule.VaccineId, cancellationToken);
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
                // Kiểm tra Vaccine có tồn tại và đang hoạt động
                var vaccine = await _vaccineRepository.GetVaccineById(vaccineScheduleRequestDTO.VaccineId, cancellationToken);
                if (vaccine == null || vaccine.IsActive != EnumList.IsActive.Active)
                {
                    throw new ArgumentException("Invalid or inactive VaccineId");
                }

                // Kiểm tra Disease có tồn tại và đang hoạt động
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

                // Gán thông tin Vaccine và Disease vào object trả về
                vaccineSchedule.Vaccine = vaccine;
                vaccineSchedule.Disease = disease;

                return _mapper.Map<VaccineScheduleResponseDTO>(vaccineSchedule);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding new vaccine schedule");
                throw new ApplicationException("Error while saving vaccine schedule", ex);
            }
        }
    }
}
