using AutoMapper;
using Microsoft.AspNetCore.Http;
using PediVax.BusinessObjects.DTO.DiseaseDTO;
using PediVax.BusinessObjects.DTO.ReponseDTO;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PediVax.Services.Service
{
    public class DiseaseService : IDiseaseService
    {
        private readonly IDiseaseRepository _diseaseRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public DiseaseService(IDiseaseRepository diseaseRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _diseaseRepository = diseaseRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        private string GetCurrentUserName()
        {
            if (_httpContextAccessor.HttpContext == null || _httpContextAccessor.HttpContext.User == null)
            {
                return "System";
            }

            var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            return string.IsNullOrEmpty(userName) ? "System" : userName;
        }

        public async Task<DiseaseResponseDTO> AddDisease(CreateDiseaseDTO createDiseaseDTO)
        {
            try
            {
                ValidateInputs(createDiseaseDTO);

                var disease = new Disease()
                {
                    Name = createDiseaseDTO.Name,
                    Description = createDiseaseDTO.Description,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = GetCurrentUserName(),
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = GetCurrentUserName(),
                    IsActive = EnumList.IsActive.Active
                };

                if (await _diseaseRepository.CreateDisease(disease) <= 0)
                {
                    throw new Exception("Failed to create disease.");
                }

                return _mapper.Map<DiseaseResponseDTO>(disease);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while saving disease: " + ex.InnerException?.Message, ex);
            }
        }

       


        public async Task<bool> DeleteDisease(int diseaseId)
        {
            return await _diseaseRepository.DeleteDisease(diseaseId);
        }

        public async Task<List<DiseaseResponseDTO>> GetAllDisease()
        {
            var disease = await _diseaseRepository.GetAllDiseases();
            return _mapper.Map<List<DiseaseResponseDTO>>(disease);
        }

        public async Task<DiseaseResponseDTO> GetDiseasebyId(int diseaseId)
        {
            var disease = await _diseaseRepository.GetDiseaseById(diseaseId);
            return _mapper.Map<DiseaseResponseDTO>(disease);
        }

        public async Task<(List<DiseaseResponseDTO> Data, int TotalCount)> GetDiseasePaged(int pageNumber, int pageSize)
        {

            var (data, totalCount) = await _diseaseRepository.GetDiseasePaged(pageNumber, pageSize);
            var mappedData = _mapper.Map<List<DiseaseResponseDTO>>(data);
            return (mappedData, totalCount);
        }

        public async Task<bool> UpdateDisease(int id, UpdateDiseaseDTO updateDiseaseDTO)
        {
            try
            {
                ValidateUpdateInputs(updateDiseaseDTO);

                var existingDisease = await _diseaseRepository.GetDiseaseById(id);
                if (existingDisease == null)
                {
                    throw new Exception("Disease not found.");
                }

                
                existingDisease.Name = updateDiseaseDTO.Name.Trim();
                existingDisease.Description = updateDiseaseDTO.Description?.Trim();
                existingDisease.ModifiedDate = DateTime.UtcNow;
                existingDisease.ModifiedBy = GetCurrentUserName();

                if (await _diseaseRepository.UpdateDisease(existingDisease) <= 0)
                {
                    throw new Exception("Failed to update disease.");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating disease: " + ex.InnerException?.Message, ex);
            }
        }
        // validate
        private void ValidateInputs(CreateDiseaseDTO createDiseaseDTO)
        {
            if (string.IsNullOrWhiteSpace(createDiseaseDTO.Name))
                throw new ArgumentException("Disease name cannot be empty.");

            if (createDiseaseDTO.Name.Length > 100)
                throw new ArgumentException("Disease name must be under 100 characters.");

            if (!string.IsNullOrWhiteSpace(createDiseaseDTO.Description) && createDiseaseDTO.Description.Length > 500)
                throw new ArgumentException("Description must be under 500 characters.");
        }

        private void ValidateUpdateInputs(UpdateDiseaseDTO updateDiseaseDTO)
        {
            if (updateDiseaseDTO == null)
                throw new ArgumentNullException(nameof(updateDiseaseDTO), "Update data cannot be null.");

            if (string.IsNullOrWhiteSpace(updateDiseaseDTO.Name))
                throw new ArgumentException("Disease name cannot be empty.");

            if (updateDiseaseDTO.Name.Length > 100)
                throw new ArgumentException("Disease name must be under 100 characters.");

            if (!Regex.IsMatch(updateDiseaseDTO.Name, @"^[a-zA-Z0-9\s]+$"))
                throw new ArgumentException("Disease name can only contain letters, numbers, and spaces.");

            if (!string.IsNullOrWhiteSpace(updateDiseaseDTO.Description) && updateDiseaseDTO.Description.Length > 500)
                throw new ArgumentException("Description must be under 500 characters.");
        }

    }
}
