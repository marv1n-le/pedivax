using PediVax.BusinessObjects.DTO.VaccineDiseaseDTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PediVax.Services.IService
{
    public interface IVaccineDiseaseService
    {
        Task<List<VaccineDiseaseResponseDTO>> GetAllVaccineDiseases(CancellationToken cancellationToken);
        Task<VaccineDiseaseResponseDTO> GetVaccineDiseaseById(int vaccineDiseaseId, CancellationToken cancellationToken);
        Task<(List<VaccineDiseaseResponseDTO> Data, int TotalCount)> GetVaccineDiseasePaged(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<VaccineDiseaseResponseDTO> AddVaccineDisease(CreateVaccineDiseaseDTO createVaccineDiseaseDTO, CancellationToken cancellationToken);
        Task<bool> UpdateVaccineDisease(int id, UpdateVaccineDiseaseDTO updateVaccineDiseaseDTO, CancellationToken cancellationToken);
        Task<bool> DeleteVaccineDisease(int id, CancellationToken cancellationToken);
    }
}
