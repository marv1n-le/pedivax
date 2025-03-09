using PediVax.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.Repositories.IRepository
{
    public interface IVaccineProfileRepository
    {
        Task<List<int>> GetCompletedDiseasesForChild(int childId, CancellationToken cancellationToken);
        Task<List<VaccineProfile>> GetAllVaccineProfile(CancellationToken cancellationToken);
        Task<VaccineProfile> GetVaccineProfileById(int id, CancellationToken cancellationToken);
        Task<VaccineProfile?> GetVaccineProfileByChildId(int childId, CancellationToken cancellationToken);
        Task<int> AddVaccineProfile(VaccineProfile vaccineProfile, CancellationToken cancellationToken);
        Task<int> UpdateVaccineProfile(VaccineProfile vaccineProfile, CancellationToken cancellationToken);
        Task<bool> DeleteVaccineProfile(int id, CancellationToken cancellationToken);
    }
}
