using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Repositories.Repository.BaseRepository;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PediVax.Repositories.Repository
{
    public class VaccinePackageRepository : GenericRepository<VaccinePackage>, IVaccinePackageRepository
    {
        public VaccinePackageRepository() : base()
        {
        }

        public async Task<List<VaccinePackage>> GetAllVaccinePackages(CancellationToken cancellationToken)
        {
            return await GetAllAsync(cancellationToken);
        }

        public async Task<VaccinePackage?> GetVaccinePackageById(int packageId, CancellationToken cancellationToken)
        {
            return await GetByIdAsync(packageId, cancellationToken);
        }

        public async Task<(List<VaccinePackage>, int)> GetVaccinePackagePaged(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await GetPagedAsync(pageNumber, pageSize, cancellationToken);
        }

        public async Task<int> CreateVaccinePackage(VaccinePackage vaccinePackage, CancellationToken cancellationToken)
        {
            return await CreateAsync(vaccinePackage, cancellationToken);
        }

        public async Task<int> UpdateVaccinePackage(VaccinePackage vaccinePackage, CancellationToken cancellationToken)
        {
            return await UpdateAsync(vaccinePackage, cancellationToken);
        }

        public async Task<bool> DeleteVaccinePackage(int packageId, CancellationToken cancellationToken)
        {
            return await DeleteAsync(packageId, cancellationToken);
        }
    }
}