﻿using System.Security.Claims;
using System.Threading;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PediVax.BusinessObjects.DTO.VaccinePackageDTO;
using PediVax.BusinessObjects.Enum;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Services.IService;

namespace PediVax.Services.Service;

public class VaccinePackageService : IVaccinePackageService
{
    private readonly IVaccinePackageRepository _vaccinePackageRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<VaccinePackageService> _logger;

    public VaccinePackageService(IVaccinePackageRepository vaccinePackageRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<VaccinePackageService> logger)
    {
        _vaccinePackageRepository = vaccinePackageRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    private string GetCurrentUserName()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "System";
    }

    private void SetAuditFields(VaccinePackage vaccinePackage)
    {
        vaccinePackage.ModifiedBy = GetCurrentUserName();
        vaccinePackage.ModifiedDate = DateTime.UtcNow;
    }

    public async Task<VaccinePackageResponseDTO> GetVaccinePackageById(int packageId, CancellationToken cancellationToken)
    {
        if (packageId <= 0)
        {
            _logger.LogWarning("Invalid package ID: {packageId}", packageId);
            throw new ArgumentException("Invalid package ID");
        }

        var vaccinePackage = await _vaccinePackageRepository.GetVaccinePackageById(packageId, cancellationToken);
        if (vaccinePackage == null)
        {
            _logger.LogWarning("Vaccine package with ID {packageId} not found", packageId);
            throw new KeyNotFoundException("Vaccine package not found");
        }

        return _mapper.Map<VaccinePackageResponseDTO>(vaccinePackage);
    }

    public async Task<VaccinePackageResponseDTO> AddVaccinePackage(CreateVaccinePackageDTO createVaccinePackageDTO, CancellationToken cancellationToken)
    {
        if (createVaccinePackageDTO == null)
        {
            throw new ArgumentNullException(nameof(createVaccinePackageDTO), "Vaccine package data is required");
        }

        try
        {
            var vaccinePackage = _mapper.Map<VaccinePackage>(createVaccinePackageDTO);
            vaccinePackage.IsActive = EnumList.IsActive.Active;
            vaccinePackage.CreatedBy = GetCurrentUserName();
            vaccinePackage.CreatedDate = DateTime.UtcNow;
            SetAuditFields(vaccinePackage);

            if (await _vaccinePackageRepository.CreateVaccinePackage(vaccinePackage, cancellationToken) <= 0)
            {
                throw new ApplicationException("Adding new vaccine package failed");
            }

            return _mapper.Map<VaccinePackageResponseDTO>(vaccinePackage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding new vaccine package");
            throw new ApplicationException("Error while saving vaccine package", ex);
        }
    }

    public async Task<bool> UpdateVaccinePackage(int id, UpdateVaccinePackageDTO updateVaccinePackageDTO, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid package ID");
        }

        if (updateVaccinePackageDTO == null)
        {
            throw new ArgumentNullException(nameof(updateVaccinePackageDTO), "Vaccine package data is required");
        }

        try
        {
            var vaccinePackage = await _vaccinePackageRepository.GetVaccinePackageById(id, cancellationToken);
            if (vaccinePackage == null)
            {
                _logger.LogWarning("Vaccine package with ID {id} not found", id);
                throw new KeyNotFoundException("Vaccine package not found");
            }

            SetAuditFields(vaccinePackage);
            vaccinePackage.Name = updateVaccinePackageDTO.Name ?? vaccinePackage.Name;
            vaccinePackage.Description = updateVaccinePackageDTO.Description ?? vaccinePackage.Description;
            vaccinePackage.TotalPrice = updateVaccinePackageDTO.TotalPrice ?? vaccinePackage.TotalPrice;

            int rowsAffected = await _vaccinePackageRepository.UpdateVaccinePackage(vaccinePackage, cancellationToken);
            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating vaccine package with ID {id}", id);
            throw new ApplicationException("Error while updating vaccine package", ex);
        }
    }

    public async Task<bool> DeleteVaccinePackage(int packageId, CancellationToken cancellationToken)
    {
        if (packageId <= 0)
        {
            throw new ArgumentException("Invalid package ID");
        }

        try
        {
            return await _vaccinePackageRepository.DeleteVaccinePackage(packageId, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting vaccine package with ID {packageId}", packageId);
            throw new ApplicationException("Error while deleting vaccine package", ex);
        }
    }

    public async Task<List<VaccinePackageResponseDTO>> GetAllVaccinePackages(CancellationToken cancellationToken)
    {
        var vaccinePackages = await _vaccinePackageRepository.GetAllVaccinePackages(cancellationToken);
        return _mapper.Map<List<VaccinePackageResponseDTO>>(vaccinePackages);
    }

    public async Task<(List<VaccinePackageResponseDTO> Data, int TotalCount)> GetVaccinePackagePaged(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0 || pageSize <= 0)
        {
            _logger.LogWarning("Invalid pagination parameters: PageNumber={pageNumber}, PageSize={pageSize}", pageNumber, pageSize);
            throw new ArgumentException("Invalid pagination parameters");
        }

        var (vaccinePackages, totalCount) = await _vaccinePackageRepository.GetVaccinePackagePaged(pageNumber, pageSize, cancellationToken);
        return (_mapper.Map<List<VaccinePackageResponseDTO>>(vaccinePackages), totalCount);
    }
}
