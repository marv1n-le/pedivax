using PediVax.BusinessObjects.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PediVax.Repositories.IRepository
{
    public interface IDiseaseRepository
    {
        Task<List<Disease>> GetAllDiseases();
        Task<Disease?> GetDiseaseById(int diseaseId);
        Task<(List<Disease>, int)> GetDiseasePaged(int pageNumber, int pageSize);
        
        Task<int> CreateDisease(Disease disease);
        Task<int> UpdateDisease(Disease disease);
        Task<bool> DeleteDisease(int diseaseId);
    }
}
