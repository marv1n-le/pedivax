using Microsoft.EntityFrameworkCore;
using PediVax.BusinessObjects.Enum;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Repositories.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.Repositories.Repository
{
    public class VaccineProfileRepository : GenericRepository<VaccineProfile>, IVaccineProfileRepository
    {
        public VaccineProfileRepository() : base()
        {

        }

        public async Task<List<int>> GetCompletedDiseasesForChild(int childId, CancellationToken cancellationToken)
        {
            var completedDiseases = await _context.VaccineProfiles
                .Where(vp => vp.ChildId == childId && vp.IsCompleted == EnumList.IsCompleted.Yes)
                .Select(vp => vp.DiseaseId)
                .ToListAsync(cancellationToken);

            return completedDiseases;
        }

        public async Task<List<VaccineProfile>> GetAllVaccineProfile(CancellationToken cancellationToken)
        {
            return await GetAllAsync(cancellationToken);
        }

        public async Task<VaccineProfile> GetVaccineProfileById(int id, CancellationToken cancellationToken)
        {
            return await GetByIdAsync(id, cancellationToken);
        }

        public async Task<VaccineProfile?> GetVaccineProfileByChildId(int childId, CancellationToken cancellationToken)
        {
            return await _context.VaccineProfiles
                .Where(vp => vp.ChildId == childId)
                .FirstOrDefaultAsync(cancellationToken);
        }


        public async Task GenerateVaccineProfile(List<VaccineProfile> vaccineProfile, CancellationToken cancellationToken)
        {
            _context.VaccineProfiles.AddRange(vaccineProfile);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> UpdateVaccineProfile(VaccineProfile vaccineProfile, CancellationToken cancellationToken)
        {
            return await UpdateAsync(vaccineProfile, cancellationToken);
        }

        public async Task<bool> DeleteVaccineProfile(int id, CancellationToken cancellationToken)
        {
            return await DeleteAsync(id, cancellationToken);
        }
    }
}
