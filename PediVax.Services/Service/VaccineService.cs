using System.Security.Claims;
using System.Threading;
using System.Transactions;
using AutoMapper;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PediVax.BusinessObjects.DTO.VaccineDTO;
using PediVax.BusinessObjects.Enum;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Services.ExternalService;
using PediVax.Services.IService;

namespace PediVax.Services.Service;

public class VaccineService : IVaccineService
{
    private readonly IVaccineRepository _vaccineRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly ILogger<VaccineService> _logger;

    public VaccineService(IVaccineRepository vaccineRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, ICloudinaryService cloudinaryService, ILogger<VaccineService> logger)
    {
        _vaccineRepository = vaccineRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _cloudinaryService = cloudinaryService;
        _logger = logger;
    }

    private string GetCurrentUserName()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "System";
    }

    private void SetAuditFields(Vaccine vaccine)
    {
        vaccine.ModifiedBy = GetCurrentUserName();
        vaccine.ModifiedDate = DateTime.UtcNow;
    }

    public async Task<List<VaccineResponseDTO>> GetAllVaccine(CancellationToken cancellationToken)
    {
        var vaccines = await _vaccineRepository.GetAllVaccine(cancellationToken);
        return _mapper.Map<List<VaccineResponseDTO>>(vaccines);
    }
    
    public async Task<VaccineResponseDTO> GetVaccineById(int vaccineId, CancellationToken cancellationToken)
    {
        if (vaccineId <= 0)
        {
            _logger.LogWarning("Invalid vaccine ID: {vaccineId}", vaccineId);
            throw new ArgumentException("Invalid vaccine ID");
        }

        var vaccine = await _vaccineRepository.GetVaccineById(vaccineId, cancellationToken);
        if (vaccine == null)
        {
            _logger.LogWarning("Vaccine with ID {vaccineId} not found", vaccineId);
            throw new KeyNotFoundException("Vaccine not found");
        }

        return _mapper.Map<VaccineResponseDTO>(vaccine);
    }
    
    public async Task<(List<VaccineResponseDTO> Data, int TotalCount)> GetVaccinePaged(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0 || pageSize <= 0)
        {
            _logger.LogWarning("Invalid pagination parameters: PageNumber={pageNumber}, PageSize={pageSize}", pageNumber, pageSize);
            throw new ArgumentException("Invalid pagination parameters");
        }

        var (vaccines, totalCount) = await _vaccineRepository.GetVaccinePaged(pageNumber, pageSize, cancellationToken);
        return (_mapper.Map<List<VaccineResponseDTO>>(vaccines), totalCount);
    }
    
    public async Task<List<VaccineResponseDTO>> GetVaccineByName(string keyword, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            throw new ArgumentException("Keyword cannot be empty");
        }

        var vaccines = await _vaccineRepository.GetVaccineByName(keyword, cancellationToken);
        return _mapper.Map<List<VaccineResponseDTO>>(vaccines);
    }
    
    public async Task<VaccineResponseDTO> AddVaccine(CreateVaccineDTO createVaccineDTO, CancellationToken cancellationToken)
    {
        if (createVaccineDTO == null)
        {
            throw new ArgumentNullException(nameof(createVaccineDTO), "Vaccine data is required");
        }

        try
        {
            var vaccine = _mapper.Map<Vaccine>(createVaccineDTO);
            if (createVaccineDTO.Image == null || createVaccineDTO.Image.Length == 0)
            {
                throw new ArgumentException("Invalid vaccine image");
            }

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            vaccine.Image = await _cloudinaryService.UploadImage(createVaccineDTO.Image);
            vaccine.Quantity = createVaccineDTO.Quantity;
            vaccine.IsActive = EnumList.IsActive.Active;
            vaccine.CreatedBy = GetCurrentUserName();
            vaccine.CreatedDate = DateTime.UtcNow;
            SetAuditFields(vaccine);

            if (await _vaccineRepository.AddVaccine(vaccine, cancellationToken) <= 0)
            {
                throw new ApplicationException("Adding new vaccine failed");
            }

            scope.Complete();
            return _mapper.Map<VaccineResponseDTO>(vaccine);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding new vaccine");
            throw new ApplicationException("Error while saving vaccine", ex);
        }
    }

    public async Task<bool> UpdateVaccine(int id, UpdateVaccineDTO updateVaccineDTO, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid vaccine ID");
        }

        if (updateVaccineDTO == null)
        {
            throw new ArgumentNullException(nameof(updateVaccineDTO), "Vaccine data is required");
        }

        try
        {
            var vaccine = await _vaccineRepository.GetVaccineById(id, cancellationToken);
            if (vaccine == null)
            {
                _logger.LogWarning("Vaccine with ID {id} not found", id);
                throw new KeyNotFoundException("Vaccine not found");
            }

            SetAuditFields(vaccine);
            vaccine.Name = updateVaccineDTO.Name ?? vaccine.Name;
            vaccine.Quantity = updateVaccineDTO.Quantity ?? vaccine.Quantity;
            vaccine.Description = updateVaccineDTO.Description ?? vaccine.Description;
            vaccine.Description = updateVaccineDTO.Description ?? vaccine.Description;
            vaccine.Origin = updateVaccineDTO.Origin ?? vaccine.Origin;
            vaccine.Price = updateVaccineDTO.Price ?? vaccine.Price;
            vaccine.DateOfManufacture = updateVaccineDTO.DateOfManufacture ?? vaccine.DateOfManufacture;

            if (updateVaccineDTO.Image != null && updateVaccineDTO.Image.Length > 0)
            {
                vaccine.Image = await _cloudinaryService.UploadImage(updateVaccineDTO.Image);
            }

            int rowAffected = await _vaccineRepository.UpdateVaccine(vaccine, cancellationToken);
            bool isUpdated = rowAffected > 0;

            return isUpdated;
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating vaccine with ID {id}", id);
            throw new ApplicationException("Error while updating vaccine", ex);
        }
    }

    public async Task<bool> DeleteVaccine(int vaccineId, CancellationToken cancellationToken)
    {
        if (vaccineId <= 0)
        {
            throw new ArgumentException("Invalid vaccine ID");
        }

        try
        {
            return await _vaccineRepository.DeleteVaccine(vaccineId, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting vaccine with ID {vaccineId}", vaccineId);
            throw new ApplicationException("Error while deleting vaccine", ex);
        }
    }
}