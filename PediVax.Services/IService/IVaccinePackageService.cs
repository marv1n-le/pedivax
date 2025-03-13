using PediVax.BusinessObjects.DTO.VaccinePackageDTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PediVax.Services.IService
{
    public interface IVaccinePackageService
    {
        Task<List<VaccinePackageResponseDTO>> GetAllVaccinePackages(CancellationToken cancellationToken);
        Task<VaccinePackageResponseDTO> GetVaccinePackageById(int packageId, CancellationToken cancellationToken);
        Task<(List<VaccinePackageResponseDTO> Data, int TotalCount)> GetVaccinePackagePaged(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<VaccinePackageResponseDTO> AddVaccinePackage(CreateVaccinePackageDTO createVaccinePackageDTO, CancellationToken cancellationToken);
        Task<bool> UpdateVaccinePackage(int id, UpdateVaccinePackageDTO updateVaccinePackageDTO, CancellationToken cancellationToken);
        Task<bool> UpdateTotalPrice(int packageId, CancellationToken cancellationToken);
        Task<bool> DeleteVaccinePackage(int id, CancellationToken cancellationToken);
    }
}
