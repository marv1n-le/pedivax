using System.Security.Claims;
using System.Threading;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PediVax.BusinessObjects.DTO.DiseaseDTO;
using PediVax.BusinessObjects.DTO.VaccineDTO;
using PediVax.BusinessObjects.Enum;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Repositories.Repository;
using PediVax.Services.IService;

namespace PediVax.Services.Service;

public class DiseaseService : IDiseaseService
{
    private readonly IDiseaseRepository _diseaseRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<DiseaseService> _logger;

    public DiseaseService(IDiseaseRepository diseaseRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<DiseaseService> logger)
    {
        _diseaseRepository = diseaseRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    private string GetCurrentUserName()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "System";
    }

    private void SetAuditFields(Disease disease)
    {
        disease.ModifiedBy = GetCurrentUserName();
        disease.ModifiedDate = DateTime.UtcNow;
    }

  

    public async Task<DiseaseResponseDTO> GetDiseaseById(int diseaseId, CancellationToken cancellationToken)
    {
        if (diseaseId <= 0)
        {
            _logger.LogWarning("Invalid disease ID: {diseaseId}", diseaseId);
            throw new ArgumentException("Invalid disease ID");
        }

        var disease = await _diseaseRepository.GetDiseaseById(diseaseId, cancellationToken);
        if (disease == null)
        {
            _logger.LogWarning("Disease with ID {diseaseId} not found", diseaseId);
            throw new KeyNotFoundException("Disease not found");
        }

        return _mapper.Map<DiseaseResponseDTO>(disease);
    }

    public async Task<DiseaseResponseDTO> AddDisease(CreateDiseaseDTO createDiseaseDTO, CancellationToken cancellationToken)
    {
        if (createDiseaseDTO == null)
        {
            throw new ArgumentNullException(nameof(createDiseaseDTO), "Disease data is required");
        }

        try
        {
            var disease = _mapper.Map<Disease>(createDiseaseDTO);
            disease.IsActive = EnumList.IsActive.Active;
            disease.CreatedBy = GetCurrentUserName();
            disease.CreatedDate = DateTime.UtcNow;
            SetAuditFields(disease);

            if (await _diseaseRepository.CreateDisease(disease, cancellationToken) <= 0)
            {
                throw new ApplicationException("Adding new disease failed");
            }

            return _mapper.Map<DiseaseResponseDTO>(disease);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding new disease");
            throw new ApplicationException("Error while saving disease", ex);
        }
    }

    public async Task<bool> UpdateDisease(int id, UpdateDiseaseDTO updateDiseaseDTO, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid disease ID");
        }

        if (updateDiseaseDTO == null)
        {
            throw new ArgumentNullException(nameof(updateDiseaseDTO), "Disease data is required");
        }

        try
        {
            var disease = await _diseaseRepository.GetDiseaseById(id, cancellationToken);
            if (disease == null)
            {
                _logger.LogWarning("Disease with ID {id} not found", id);
                throw new KeyNotFoundException("Disease not found");
            }

            SetAuditFields(disease);
            disease.Name = updateDiseaseDTO.Name ?? disease.Name;
            disease.Description = updateDiseaseDTO.Description ?? disease.Description;

            int rowsAffected = await _diseaseRepository.UpdateDisease(disease, cancellationToken);
            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating disease with ID {id}", id);
            throw new ApplicationException("Error while updating disease", ex);
        }
    }

    public async Task<bool> DeleteDisease(int diseaseId, CancellationToken cancellationToken)
    {
        if (diseaseId <= 0)
        {
            throw new ArgumentException("Invalid disease ID");
        }

        try
        {
            return await _diseaseRepository.DeleteDisease(diseaseId, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting disease with ID {diseaseId}", diseaseId);
            throw new ApplicationException("Error while deleting disease", ex);
        }
    }

    public async Task<List<DiseaseResponseDTO>> GetAllDiseases(CancellationToken cancellationToken)
    {
        var diseases = await _diseaseRepository.GetAllDiseases(cancellationToken);
        return _mapper.Map<List<DiseaseResponseDTO>>(diseases);
    }

    public async Task<(List<DiseaseResponseDTO> Data, int TotalCount)> GetDiseasePaged(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0 || pageSize <= 0)
        {
            _logger.LogWarning("Invalid pagination parameters: PageNumber={pageNumber}, PageSize={pageSize}", pageNumber, pageSize);
            throw new ArgumentException("Invalid pagination parameters");
        }

        var (diseases, totalCount) = await _diseaseRepository.GetDiseasePaged(pageNumber, pageSize, cancellationToken);
        return (_mapper.Map<List<DiseaseResponseDTO>>(diseases), totalCount);
    }
}
