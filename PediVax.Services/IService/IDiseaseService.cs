using PediVax.BusinessObjects.DTO.DiseaseDTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PediVax.Services.IService
{
    public interface IDiseaseService
    {
        Task<List<DiseaseResponseDTO>> GetAllDiseases(CancellationToken cancellationToken);
        Task<DiseaseResponseDTO> GetDiseaseById(int diseaseId, CancellationToken cancellationToken);
        Task<(List<DiseaseResponseDTO> Data, int TotalCount)> GetDiseasePaged(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<DiseaseResponseDTO> AddDisease(CreateDiseaseDTO createDiseaseDTO, CancellationToken cancellationToken);
        Task<bool> UpdateDisease(int id, UpdateDiseaseDTO updateDiseaseDTO, CancellationToken cancellationToken);
        Task<bool> DeleteDisease(int id, CancellationToken cancellationToken);
    }
}
