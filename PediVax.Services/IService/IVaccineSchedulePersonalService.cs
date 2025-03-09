using PediVax.BusinessObjects.DTO.VaccineSchedulePersonalDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.Services.IService
{
    public interface IVaccineSchedulePersonalService
    {
        Task<List<VaccineSchedulePersonalResponseDTO>> GetAllVaccineSchedulePersonal(CancellationToken cancellationToken);
        Task<List<VaccineSchedulePersonalResponseDTO>> GetVaccineSchedulePersonalByChildId(int childId, CancellationToken cancellationToken);
        Task<List<VaccineSchedulePersonalResponseDTO>> GenerateVaccineScheduleForChild(int childId, CancellationToken cancellationToken);
        Task<bool> DeleteVaccineSchedulePersonal(int vaccineSchedulePersonalId, CancellationToken cancellationToken);
    }
}
