using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Repositories.Repository.BaseRepository;

namespace PediVax.Repositories.Repository;

public class ChildProfileRepository : GenericRepository<ChildProfile>, IChildProfileRepository
{
    public ChildProfileRepository() : base() {}

    public async Task<List<ChildProfile>> GetAllChildProfiles()
    {
        return await GetAllAsync();
    }
    
    public async Task<ChildProfile> GetChildProfileById(int childProfileId)
    {
        return await GetByIdAsync(childProfileId);
    }
    
    public async Task<int> CreateChildProfile(ChildProfile childProfile)
    {
        return await CreateAsync(childProfile);
    }
    
    public async Task<int> UpdateChildProfile(ChildProfile childProfile)
    {
        return await UpdateAsync(childProfile);
    }
    
    public async Task<bool> DeleteChildProfile(int childProfileId)
    {
        return await DeleteAsync(childProfileId);
    }

    public async Task<List<ChildProfile>> GetChildByName(string keyword)
    {
        return await GetByNameContainingAsync(keyword);
    }
}