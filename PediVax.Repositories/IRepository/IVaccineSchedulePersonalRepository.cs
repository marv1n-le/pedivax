using PediVax.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.Repositories.IRepository
{
    public interface IVaccineSchedulePersonalRepository
    {
        Task<List<VaccineSchedulePersonal>> GetAllVaccineSchedulePersonal(CancellationToken cancellationToken);
        Task<VaccineSchedulePersonal> GetVaccineSchedulePersonalById(int id, CancellationToken cancellationToken);
        Task<List<VaccineSchedulePersonal>> GetVaccineSchedulePersonalByChildId(int childId, CancellationToken cancellationToken);
        Task AddVaccineSchedulePersonals(List<VaccineSchedulePersonal> vaccineSchedulePersonals, CancellationToken cancellationToken);
        Task<bool> DeleteVaccineSchedulePersonal(int id, CancellationToken cancellationToken);
    }
}
