using PediVax.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.Repositories.IRepository
{
    public interface IVaccineScheduleRepository
    {
        Task<List<VaccineSchedule>> GetAllVaccineSchedule(CancellationToken cancellationToken);
        Task<VaccineSchedule> GetVaccineScheduleById(int id, CancellationToken cancellationToken);
        Task<int> AddVaccineSchedule(VaccineSchedule vaccineSchedule, CancellationToken cancellationToken);
        Task<int> UpdateVaccineSchedule(VaccineSchedule vaccineSchedule, CancellationToken cancellationToken);
        Task<bool> DeleteVaccineSchedule(int id, CancellationToken cancellationToken);
    }
}
