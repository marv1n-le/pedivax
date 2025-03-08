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
    public class VaccineScheduleRepository : GenericRepository<VaccineSchedule>, IVaccineScheduleRepository
    {
        public VaccineScheduleRepository() : base()
        {

        }

        public async Task<List<VaccineSchedule>> GetAllVaccineSchedule(CancellationToken cancellationToken)
        {
            return await GetAllAsync(cancellationToken);
        }

        public async Task<VaccineSchedule> GetVaccineScheduleById(int id, CancellationToken cancellationToken)
        {   
            return await GetByIdAsync(id, cancellationToken);
        }

        public async Task<int> AddVaccineSchedule(VaccineSchedule vaccineSchedule, CancellationToken cancellationToken)
        {
            return await CreateAsync(vaccineSchedule, cancellationToken);
        }

        public async Task<int> UpdateVaccineSchedule(VaccineSchedule vaccineSchedule, CancellationToken cancellationToken)
        {
            return await UpdateAsync(vaccineSchedule, cancellationToken);
        }

        public async Task<bool> DeleteVaccineSchedule(int id, CancellationToken cancellationToken)
        {
            return await DeleteAsync(id, cancellationToken);
        }
    }   
}
