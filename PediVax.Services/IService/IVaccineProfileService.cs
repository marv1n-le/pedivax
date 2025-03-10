using PediVax.BusinessObjects.DTO.VaccineProfileDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.Services.IService
{
    public interface IVaccineProfileService
    {
        Task<List<VaccineProfileResponseDTO>> GetAllVaccineProfiles(CancellationToken cancellationToken);
        Task<VaccineProfileResponseDTO> GetVaccineProfileById(int id, CancellationToken cancellationToken);
        Task<VaccineProfileResponseDTO> AddVaccineProfile(CreateVaccineProfileDTO vaccineProfileDTO, CancellationToken cancellationToken);
        Task<bool> UpdateVaccineProfile(int id, UpdateVaccineProfileDTO updateVaccineProfileDTO, CancellationToken cancellationToken);
        Task<bool> DeleteVaccineProfile(int id, CancellationToken cancellationToken);

    }
}
