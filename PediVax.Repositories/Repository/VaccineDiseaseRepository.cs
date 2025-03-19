using Microsoft.EntityFrameworkCore;
using PediVax.BusinessObjects.DBContext;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Repositories.Repository.BaseRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PediVax.Repositories.Repository
{
    public class VaccineDiseaseRepository : GenericRepository<VaccineDisease>, IVaccineDiseaseRepository
    {
        public VaccineDiseaseRepository() : base()
        {
        }

        public async Task<List<VaccineDisease>> GetAllVaccineDiseases(CancellationToken cancellationToken)
        {
            return await _context.VaccineDiseases
                .Include(vd => vd.Vaccine)
                .Include(vd => vd.Disease)
                .ToListAsync(cancellationToken);
        }

        public async Task<VaccineDisease?> GetVaccineDiseaseById(int vaccineDiseaseId, CancellationToken cancellationToken)
        {
            return await _context.VaccineDiseases
                .Include(vd => vd.Vaccine)
                .Include(vd => vd.Disease)
                .FirstOrDefaultAsync(vd => vd.VaccineDiseaseId == vaccineDiseaseId, cancellationToken);
        }

        public async Task<(List<VaccineDisease>, int)> GetVaccineDiseasePaged(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = _context.VaccineDiseases
                .Include(vd => vd.Vaccine)
                .Include(vd => vd.Disease);

            int totalCount = await query.CountAsync(cancellationToken);
            List<VaccineDisease> data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (data, totalCount);
        }

        public async Task<List<VaccineDisease>> GetVaccineDiseasesByVaccineId(int vaccineId, CancellationToken cancellationToken)
        {
            return await _context.VaccineDiseases
                .Where(vd => vd.VaccineId == vaccineId)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> CreateVaccineDisease(VaccineDisease vaccineDisease, CancellationToken cancellationToken)
        {
            _context.VaccineDiseases.Add(vaccineDisease);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> UpdateVaccineDisease(VaccineDisease vaccineDisease, CancellationToken cancellationToken)
        {
            _context.VaccineDiseases.Update(vaccineDisease);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> DeleteVaccineDisease(int vaccineDiseaseId, CancellationToken cancellationToken)
        {
            var entity = await _context.VaccineDiseases.FindAsync(new object[] { vaccineDiseaseId }, cancellationToken);
            if (entity == null)
            {
                return false;
            }

            _context.VaccineDiseases.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
