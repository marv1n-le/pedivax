using PediVax.BusinessObjects.DTO.DiseaseDTO;
using PediVax.BusinessObjects.DTO.ReponseDTO;
using PediVax.BusinessObjects.DTO.RequestDTO;

namespace PediVax.Services.IService
{
    public interface IDiseaseService
    {
        Task<List<DiseaseResponseDTO>> GetAllDisease();
        Task<DiseaseResponseDTO> AddDisease(CreateDiseaseDTO createDiseaseDTO);
        Task<DiseaseResponseDTO> GetDiseasebyId(int diseaseId);
        Task<bool> DeleteDisease(int diseaseId);
        Task<bool> UpdateDisease(int id, UpdateDiseaseDTO updateDiseaseDTO);
        Task<(List<DiseaseResponseDTO> Data, int TotalCount)> GetDiseasePaged(int pageNumber, int pageSize);

    }
}
