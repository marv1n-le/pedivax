using PediVax.BusinessObjects.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PediVax.Repositories.IRepository
{
    public interface IVaccinePackageDetailRepository
    {
        Task<List<VaccinePackageDetail>> GetAllVaccinePackageDetails(CancellationToken cancellationToken);
        Task<VaccinePackageDetail?> GetVaccinePackageDetailById(int packageDetailId, CancellationToken cancellationToken);
        Task<(List<VaccinePackageDetail>, int)> GetVaccinePackageDetailPaged(int pageNumber, int pageSize, CancellationToken cancellationToken);

        Task<int> CreateVaccinePackageDetail(VaccinePackageDetail vaccinePackageDetail, CancellationToken cancellationToken);
        Task<int> UpdateVaccinePackageDetail(VaccinePackageDetail vaccinePackageDetail, CancellationToken cancellationToken);
        Task<bool> DeleteVaccinePackageDetail(int packageDetailId, CancellationToken cancellationToken);
    }
}
