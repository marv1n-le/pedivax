using System.Security.Claims;
using System.Threading;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PediVax.BusinessObjects.DTO.VaccinationRecordDTo;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Services.IService;
using PediVax.BusinessObjects.Enum;

namespace PediVax.Services.Service
{
    public class VaccinationRecordService : IVaccinationRecordService
    {
        private readonly IVaccinationRecordRepository _vaccinationRecordRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<VaccinationRecordService> _logger;

        public VaccinationRecordService(IVaccinationRecordRepository vaccinationRecordRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<VaccinationRecordService> logger)
        {
            _vaccinationRecordRepository = vaccinationRecordRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        private string GetCurrentUserName()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "System";
        }

        private void SetAuditFields(VaccinationRecord vaccinationRecord)
        {
            vaccinationRecord.ModifiedBy = GetCurrentUserName();
            vaccinationRecord.ModifiedDate = DateTime.UtcNow;
        }

        public async Task<VaccinationRecordRequestDTO> GetVaccinationRecordById(int recordId, CancellationToken cancellationToken)
        {
            if (recordId <= 0)
            {
                _logger.LogWarning("Invalid record ID: {recordId}", recordId);
                throw new ArgumentException("Invalid record ID");
            }

            var vaccinationRecord = await _vaccinationRecordRepository.GetVaccinationRecordById(recordId, cancellationToken);
            if (vaccinationRecord == null)
            {
                _logger.LogWarning("Vaccination record with ID {recordId} not found", recordId);
                throw new KeyNotFoundException("Vaccination record not found");
            }

            return _mapper.Map<VaccinationRecordRequestDTO>(vaccinationRecord);
        }

        public async Task<int> AddVaccinationRecord(CreateVaccinationRecordDTO createVaccinationRecordDTO, CancellationToken cancellationToken)
        {
            if (createVaccinationRecordDTO == null)
            {
                throw new ArgumentNullException(nameof(createVaccinationRecordDTO), "Vaccination record data is required");
            }

            try
            {
                var vaccinationRecord = _mapper.Map<VaccinationRecord>(createVaccinationRecordDTO);
                vaccinationRecord.IsActive = EnumList.IsActive.Active; // Sử dụng EnumList để set IsActive
                vaccinationRecord.CreatedBy = GetCurrentUserName();
                vaccinationRecord.CreatedDate = DateTime.UtcNow;
                SetAuditFields(vaccinationRecord);

                int recordId = await _vaccinationRecordRepository.AddVaccinationRecord(vaccinationRecord, cancellationToken);
                return recordId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding new vaccination record");
                throw new ApplicationException("Error while saving vaccination record", ex);
            }
        }

        public async Task<bool> UpdateVaccinationRecord(int recordId, UpdateVaccinationRecordDTO updateVaccinationRecordDTO, CancellationToken cancellationToken)
        {
            if (recordId <= 0)
            {
                throw new ArgumentException("Invalid record ID");
            }

            if (updateVaccinationRecordDTO == null)
            {
                throw new ArgumentNullException(nameof(updateVaccinationRecordDTO), "Vaccination record data is required");
            }

            try
            {
                var vaccinationRecord = await _vaccinationRecordRepository.GetVaccinationRecordById(recordId, cancellationToken);
                if (vaccinationRecord == null)
                {
                    _logger.LogWarning("Vaccination record with ID {recordId} not found", recordId);
                    throw new KeyNotFoundException("Vaccination record not found");
                }

                SetAuditFields(vaccinationRecord);
                vaccinationRecord.Reaction = updateVaccinationRecordDTO.Reaction ?? vaccinationRecord.Reaction;
                vaccinationRecord.Notes = updateVaccinationRecordDTO.Notes ?? vaccinationRecord.Notes;

                int rowsAffected = await _vaccinationRecordRepository.UpdateVaccinationRecord(vaccinationRecord, cancellationToken);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating vaccination record with ID {recordId}", recordId);
                throw new ApplicationException("Error while updating vaccination record", ex);
            }
        }

        public async Task<bool> DeleteVaccinationRecord(int recordId, CancellationToken cancellationToken)
        {
            if (recordId <= 0)
            {
                throw new ArgumentException("Invalid record ID");
            }

            try
            {
                return await _vaccinationRecordRepository.DeleteVaccinationRecord(recordId, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting vaccination record with ID {recordId}", recordId);
                throw new ApplicationException("Error while deleting vaccination record", ex);
            }
        }

        public async Task<List<VaccinationRecordRequestDTO>> GetAllVaccinationRecords(CancellationToken cancellationToken)
        {
            var vaccinationRecords = await _vaccinationRecordRepository.GetAllVaccinationRecords(cancellationToken);
            return _mapper.Map<List<VaccinationRecordRequestDTO>>(vaccinationRecords);
        }

        public async Task<(List<VaccinationRecordRequestDTO> Data, int TotalCount)> GetVaccinationRecordsPaged(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                _logger.LogWarning("Invalid pagination parameters: PageNumber={pageNumber}, PageSize={pageSize}", pageNumber, pageSize);
                throw new ArgumentException("Invalid pagination parameters");
            }

            var (vaccinationRecords, totalCount) = await _vaccinationRecordRepository.GetVaccinationRecordsPaged(pageNumber, pageSize, cancellationToken);
            return (_mapper.Map<List<VaccinationRecordRequestDTO>>(vaccinationRecords), totalCount);
        }
    }
}
