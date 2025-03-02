using PediVax.BusinessObjects.Models;

namespace PediVax.Repositories.IRepository;

public interface IChildProfileRepository
{
    Task<List<ChildProfile>> GetAllChildProfiles();
    Task<ChildProfile> GetChildProfileById(int childProfileId);
    Task<int> CreateChildProfile(ChildProfile childProfile);
    Task<int> UpdateChildProfile(ChildProfile childProfile);
    Task<bool> DeleteChildProfile(int childProfileId);
}