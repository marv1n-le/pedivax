using PediVax.BusinessObjects.DTO.VaccinePackageDetailDTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PediVax.Services.IService
{
    public interface IVaccinePackageDetailService
    {
        Task<List<VaccinePackageDetailResponseDTO>> GetAllVaccinePackageDetails(CancellationToken cancellationToken);
        Task<VaccinePackageDetailResponseDTO> GetVaccinePackageDetailById(int packageDetailId, CancellationToken cancellationToken);
        Task<(List<VaccinePackageDetailResponseDTO> Data, int TotalCount)> GetVaccinePackageDetailPaged(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<VaccinePackageDetailResponseDTO> AddVaccinePackageDetail(CreateVaccinePackageDetailDTO createVaccinePackageDetailDTO, CancellationToken cancellationToken);
        Task<bool> UpdateVaccinePackageDetail(int id, UpdateVaccinePackageDetailDTO updateVaccinePackageDetailDTO, CancellationToken cancellationToken);
        Task<bool> DeleteVaccinePackageDetail(int id, CancellationToken cancellationToken);
    }
}
