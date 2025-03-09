using Microsoft.EntityFrameworkCore;
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
    public class VaccineSchedulePersonalRepository : GenericRepository<VaccineSchedulePersonal>, IVaccineSchedulePersonalRepository
    {
        public VaccineSchedulePersonalRepository() : base()
        {
        }

        public async Task<List<VaccineSchedulePersonal>> GetAllVaccineSchedulePersonal(CancellationToken cancellationToken)
        {
            return await GetAllAsync(cancellationToken);
        }

        public async Task<VaccineSchedulePersonal> GetVaccineSchedulePersonalById(int id, CancellationToken cancellationToken)
        {
            return await GetByIdAsync(id, cancellationToken);
        }

        public async Task<List<VaccineSchedulePersonal>> GetVaccineSchedulePersonalByChildId(int childId, CancellationToken cancellationToken)
        {
            return await _context.VaccineSchedulePersonals
                .Where(vp => vp.ChildId == childId)
                .ToListAsync(cancellationToken);
        }

        public async Task AddVaccineSchedulePersonals(List<VaccineSchedulePersonal> vaccineSchedulePersonals, CancellationToken cancellationToken)
        {
            _context.VaccineSchedulePersonals.AddRange(vaccineSchedulePersonals);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> DeleteVaccineSchedulePersonal(int id, CancellationToken cancellationToken)
        {
            return await DeleteAsync(id, cancellationToken);
        }
    }
}
