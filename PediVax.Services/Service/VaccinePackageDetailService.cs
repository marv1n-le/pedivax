using System.Security.Claims;
using System.Threading;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PediVax.BusinessObjects.DTO.VaccinePackageDetailDTO;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Services.IService;

namespace PediVax.Services.Service;

public class VaccinePackageDetailService : IVaccinePackageDetailService
{
    private readonly IVaccinePackageDetailRepository _vaccinePackageDetailRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<VaccinePackageDetailService> _logger;

    public VaccinePackageDetailService(IVaccinePackageDetailRepository vaccinePackageDetailRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<VaccinePackageDetailService> logger)
    {
        _vaccinePackageDetailRepository = vaccinePackageDetailRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    private string GetCurrentUserName()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "System";
    }

    public async Task<VaccinePackageDetailResponseDTO> GetVaccinePackageDetailById(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            _logger.LogWarning("Invalid VaccinePackageDetail ID: {id}", id);
            throw new ArgumentException("Invalid VaccinePackageDetail ID");
        }

        var vaccinePackageDetail = await _vaccinePackageDetailRepository.GetVaccinePackageDetailById(id, cancellationToken);
        if (vaccinePackageDetail == null)
        {
            _logger.LogWarning("VaccinePackageDetail with ID {id} not found", id);
            throw new KeyNotFoundException("VaccinePackageDetail not found");
        }

        return _mapper.Map<VaccinePackageDetailResponseDTO>(vaccinePackageDetail);
    }

    public async Task<VaccinePackageDetailResponseDTO> AddVaccinePackageDetail(CreateVaccinePackageDetailDTO createDTO, CancellationToken cancellationToken)
    {
        if (createDTO == null)
        {
            throw new ArgumentNullException(nameof(createDTO), "VaccinePackageDetail data is required");
        }

        try
        {
            var vaccinePackageDetail = _mapper.Map<VaccinePackageDetail>(createDTO);
            vaccinePackageDetail.IsActive = "Active";

            if (await _vaccinePackageDetailRepository.CreateVaccinePackageDetail(vaccinePackageDetail, cancellationToken) <= 0)
            {
                throw new ApplicationException("Adding new VaccinePackageDetail failed");
            }

            return _mapper.Map<VaccinePackageDetailResponseDTO>(vaccinePackageDetail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding new VaccinePackageDetail");
            throw new ApplicationException("Error while saving VaccinePackageDetail", ex);
        }
    }

    public async Task<bool> UpdateVaccinePackageDetail(int id, UpdateVaccinePackageDetailDTO updateVaccinePackageDetailDTO, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid VaccinePackageDetail ID");
        }

        if (updateVaccinePackageDetailDTO == null)
        {
            throw new ArgumentNullException(nameof(updateVaccinePackageDetailDTO), "Update data is required");
        }

        try
        {
            var vaccinePackageDetail = await _vaccinePackageDetailRepository.GetVaccinePackageDetailById(id, cancellationToken);
            if (vaccinePackageDetail == null)
            {
                _logger.LogWarning("VaccinePackageDetail with ID {id} not found", id);
                throw new KeyNotFoundException("VaccinePackageDetail not found");
            }

            // Chỉ cập nhật Quantity nếu có giá trị mới, giữ nguyên ID và các thuộc tính khác
            vaccinePackageDetail.Quantity = updateVaccinePackageDetailDTO.Quantity ?? vaccinePackageDetail.Quantity;


            int rowsAffected = await _vaccinePackageDetailRepository.UpdateVaccinePackageDetail(vaccinePackageDetail, cancellationToken);
            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating VaccinePackageDetail with ID {id}", id);
            throw new ApplicationException("Error while updating VaccinePackageDetail", ex);
        }
    }

    public async Task<bool> DeleteVaccinePackageDetail(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid VaccinePackageDetail ID");
        }

        try
        {
            return await _vaccinePackageDetailRepository.DeleteVaccinePackageDetail(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting VaccinePackageDetail with ID {id}", id);
            throw new ApplicationException("Error while deleting VaccinePackageDetail", ex);
        }
    }

    public async Task<List<VaccinePackageDetailResponseDTO>> GetAllVaccinePackageDetails(CancellationToken cancellationToken)
    {
        var details = await _vaccinePackageDetailRepository.GetAllVaccinePackageDetails(cancellationToken);
        return _mapper.Map<List<VaccinePackageDetailResponseDTO>>(details);
    }

    public async Task<(List<VaccinePackageDetailResponseDTO> Data, int TotalCount)> GetVaccinePackageDetailPaged(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0 || pageSize <= 0)
        {
            _logger.LogWarning("Invalid pagination parameters: PageNumber={pageNumber}, PageSize={pageSize}", pageNumber, pageSize);
            throw new ArgumentException("Invalid pagination parameters");
        }

        var (details, totalCount) = await _vaccinePackageDetailRepository.GetVaccinePackageDetailPaged(pageNumber, pageSize, cancellationToken);
        return (_mapper.Map<List<VaccinePackageDetailResponseDTO>>(details), totalCount);
    }
}
