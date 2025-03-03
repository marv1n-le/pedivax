using PediVax.BusinessObjects.DTO.VaccineDTO;

namespace PediVax.Services.IService;

public interface IVaccineService
{
    Task<List<VaccineResponseDTO>> GetAllVaccine(CancellationToken cancellationToken);
    Task<VaccineResponseDTO> GetVaccineById(int vaccineId, CancellationToken cancellationToken);
    Task<(List<VaccineResponseDTO> Data, int TotalCount)> GetVaccinePaged(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<List<VaccineResponseDTO>> GetVaccineByName(string keyword, CancellationToken cancellationToken);
    Task<VaccineResponseDTO> AddVaccine(CreateVaccineDTO createVaccineDTO, CancellationToken cancellationToken);  
    Task<bool> UpdateVaccine(int id, UpdateVaccineDTO updateVaccineDTO, CancellationToken cancellationToken);
    Task<bool> DeleteVaccine(int id, CancellationToken cancellationToken);
}