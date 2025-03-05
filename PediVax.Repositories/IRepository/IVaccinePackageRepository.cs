using PediVax.BusinessObjects.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PediVax.Repositories.IRepository
{
    public interface IVaccinePackageRepository
    {
        Task<List<VaccinePackage>> GetAllVaccinePackages(CancellationToken cancellationToken);
        Task<VaccinePackage?> GetVaccinePackageById(int packageId, CancellationToken cancellationToken);
        Task<(List<VaccinePackage>, int)> GetVaccinePackagePaged(int pageNumber, int pageSize, CancellationToken cancellationToken);

        Task<int> CreateVaccinePackage(VaccinePackage vaccinePackage, CancellationToken cancellationToken);
        Task<int> UpdateVaccinePackage(VaccinePackage vaccinePackage, CancellationToken cancellationToken);
        Task<bool> DeleteVaccinePackage(int packageId, CancellationToken cancellationToken);
    }
}
