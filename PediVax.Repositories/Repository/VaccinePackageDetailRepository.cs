using Microsoft.EntityFrameworkCore;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Repositories.Repository.BaseRepository;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PediVax.Repositories.Repository
{
    public class VaccinePackageDetailRepository : GenericRepository<VaccinePackageDetail>, IVaccinePackageDetailRepository
    {
        public VaccinePackageDetailRepository() : base()
        {
        }

        public async Task<List<VaccinePackageDetail>> GetAllVaccinePackageDetails(CancellationToken cancellationToken)
        {
            return await GetAllAsync(cancellationToken);
        }

        public async Task<VaccinePackageDetail?> GetVaccinePackageDetailById(int packageDetailId, CancellationToken cancellationToken)
        {
            return await GetByIdAsync(packageDetailId, cancellationToken);
        }

        public async Task<List<VaccinePackageDetail>> GetVaccinePackageDetailByPackageId(int packageId, CancellationToken cancellationToken)
        {
            return await _context.VaccinePackageDetails
                .Where(vpd => vpd.VaccinePackageId == packageId)
                .ToListAsync(cancellationToken);
        }

        public async Task<(List<VaccinePackageDetail>, int)> GetVaccinePackageDetailPaged(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await GetPagedAsync(pageNumber, pageSize, cancellationToken);
        }

        public async Task<int> CreateVaccinePackageDetail(VaccinePackageDetail vaccinePackageDetail, CancellationToken cancellationToken)
        {
            return await CreateAsync(vaccinePackageDetail, cancellationToken);
        }

        public async Task<int> UpdateVaccinePackageDetail(VaccinePackageDetail vaccinePackageDetail, CancellationToken cancellationToken)
        {
            return await UpdateAsync(vaccinePackageDetail, cancellationToken);
        }

        public async Task<bool> DeleteVaccinePackageDetail(int packageDetailId, CancellationToken cancellationToken)
        {
            return await DeleteAsync(packageDetailId, cancellationToken);
        }
    }
}
