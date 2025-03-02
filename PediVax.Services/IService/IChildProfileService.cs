using PediVax.BusinessObjects.DTO.ChildProfileDTO;

namespace PediVax.Services.IService;

public interface IChildProfileService
{
    Task<List<ChildProfileResponseDTO>> GetAllChildProfiles();
    Task<ChildProfileResponseDTO> GetChildProfileById(int childProfileId);
    Task<(List<ChildProfileResponseDTO> Data, int TotalCount)> GetChildProfilePaged(int pageNumber, int pageSize);
    Task<List<ChildProfileResponseDTO>> GetChildByName(string keyword);
    Task<ChildProfileResponseDTO> CreateChildProfile(CreateChildProfileDTO createChildProfileDTO);
    Task<bool> UpdateChildProfile(int id, UpdateChildProfileDTO updateChildProfileDTO);
    Task<bool> DeleteChildProfile(int childProfileId);
}