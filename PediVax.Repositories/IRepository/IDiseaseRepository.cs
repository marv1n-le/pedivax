using PediVax.BusinessObjects.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PediVax.Repositories.IRepository
{
    public interface IDiseaseRepository
    {
        Task<List<Disease>> GetAllDiseases(CancellationToken cancellationToken);
        Task<Disease?> GetDiseaseById(int diseaseId, CancellationToken cancellationToken);
        Task<(List<Disease>, int)> GetDiseasePaged(int pageNumber, int pageSize, CancellationToken cancellationToken);

        Task<int> CreateDisease(Disease disease, CancellationToken cancellationToken);
        Task<int> UpdateDisease(Disease disease, CancellationToken cancellationToken);
        Task<bool> DeleteDisease(int diseaseId, CancellationToken cancellationToken);
    }
}
