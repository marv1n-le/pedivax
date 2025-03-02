using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Repositories.Repository.BaseRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PediVax.Repositories.Repository;

public class DiseaseRepository : GenericRepository<Disease>, IDiseaseRepository
{
    public DiseaseRepository() : base() { }

    public async Task<int> CreateDisease(Disease disease)
    {
        return await CreateAsync(disease);
    }

    public async Task<bool> DeleteDisease(int diseaseId)
    {
        return await DeleteAsync(diseaseId);
    }

    public async Task<List<Disease>> GetAllDiseases()
    {
        return await GetAllAsync();
    }

    public async Task<Disease?> GetDiseaseById(int diseaseId)
    {
        return await GetByIdAsync(diseaseId);
    }

   

    public async Task<(List<Disease>, int)> GetDiseasePaged(int pageNumber, int pageSize)
    {
        return await GetPagedAsync(pageNumber, pageSize);
    }

    public async Task<int> UpdateDisease(Disease disease)
    {
        return await UpdateAsync(disease);
    }
}
