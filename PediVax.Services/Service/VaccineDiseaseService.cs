using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PediVax.BusinessObjects.DTO.VaccineDiseaseDTO;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Services.IService;

namespace PediVax.Services.Service
{
    public class VaccineDiseaseService : IVaccineDiseaseService
    {
        private readonly IVaccineDiseaseRepository _vaccineDiseaseRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<VaccineDiseaseService> _logger;

        public VaccineDiseaseService(IVaccineDiseaseRepository vaccineDiseaseRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<VaccineDiseaseService> logger)
        {
            _vaccineDiseaseRepository = vaccineDiseaseRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        private string GetCurrentUserName()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "System";
        }

        private void SetAuditFields(VaccineDisease vaccineDisease)
        {
            vaccineDisease.ModifiedDate = DateTime.UtcNow;
        }

        public async Task<VaccineDiseaseResponseDTO> GetVaccineDiseaseById(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID");
            }

            var vaccineDisease = await _vaccineDiseaseRepository.GetVaccineDiseaseById(id, cancellationToken);
            if (vaccineDisease == null)
            {
                throw new KeyNotFoundException("VaccineDisease not found");
            }

            return _mapper.Map<VaccineDiseaseResponseDTO>(vaccineDisease);
        }

        public async Task<VaccineDiseaseResponseDTO> AddVaccineDisease(CreateVaccineDiseaseDTO createVaccineDiseaseDTO, CancellationToken cancellationToken)
        {
            if (createVaccineDiseaseDTO == null)
            {
                throw new ArgumentNullException(nameof(createVaccineDiseaseDTO), "Data is required");
            }

            var vaccineDisease = _mapper.Map<VaccineDisease>(createVaccineDiseaseDTO);
            vaccineDisease.CreatedBy = GetCurrentUserName();
            vaccineDisease.CreatedDate = DateTime.UtcNow;
            SetAuditFields(vaccineDisease);

            if (await _vaccineDiseaseRepository.CreateVaccineDisease(vaccineDisease, cancellationToken) <= 0)
            {
                throw new ApplicationException("Adding new VaccineDisease failed");
            }

            return _mapper.Map<VaccineDiseaseResponseDTO>(vaccineDisease);
        }

        public async Task<bool> UpdateVaccineDisease(int id, UpdateVaccineDiseaseDTO updateVaccineDiseaseDTO, CancellationToken cancellationToken)
        {
            if (id <= 0 || updateVaccineDiseaseDTO == null)
            {
                throw new ArgumentException("Invalid data");
            }

            var vaccineDisease = await _vaccineDiseaseRepository.GetVaccineDiseaseById(id, cancellationToken);
            if (vaccineDisease == null)
            {
                throw new KeyNotFoundException("VaccineDisease not found");
            }

            SetAuditFields(vaccineDisease);
            vaccineDisease.VaccineId = updateVaccineDiseaseDTO.VaccineId;
            vaccineDisease.DiseaseId = updateVaccineDiseaseDTO.DiseaseId;

            int rowsAffected = await _vaccineDiseaseRepository.UpdateVaccineDisease(vaccineDisease, cancellationToken);
            return rowsAffected > 0;
        }

        

        public async Task<List<VaccineDiseaseResponseDTO>> GetAllVaccineDiseases(CancellationToken cancellationToken)
        {
            var vaccineDiseases = await _vaccineDiseaseRepository.GetAllVaccineDiseases(cancellationToken);
            return _mapper.Map<List<VaccineDiseaseResponseDTO>>(vaccineDiseases);
        }

        public async Task<(List<VaccineDiseaseResponseDTO> Data, int TotalCount)> GetVaccineDiseasePaged(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                throw new ArgumentException("Invalid pagination parameters");
            }

            var (vaccineDiseases, totalCount) = await _vaccineDiseaseRepository.GetVaccineDiseasePaged(pageNumber, pageSize, cancellationToken);
            return (_mapper.Map<List<VaccineDiseaseResponseDTO>>(vaccineDiseases), totalCount);
        }

        public async Task<bool> DeleteVaccineDisease(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID");
            }

            var vaccineDisease = await _vaccineDiseaseRepository.GetVaccineDiseaseById(id, cancellationToken);
            if (vaccineDisease == null)
            {
                throw new KeyNotFoundException("VaccineDisease not found");
            }

            return await _vaccineDiseaseRepository.DeleteVaccineDisease(id, cancellationToken);
        }


    }
}
