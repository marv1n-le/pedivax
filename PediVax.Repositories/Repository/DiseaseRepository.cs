using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Repositories.Repository.BaseRepository;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PediVax.Repositories.Repository
{
    public class DiseaseRepository : GenericRepository<Disease>, IDiseaseRepository
    {
        public DiseaseRepository() : base()
        {
        }

        public async Task<List<Disease>> GetAllDiseases(CancellationToken cancellationToken)
        {
            return await GetAllAsync(cancellationToken);
        }

        public async Task<Disease?> GetDiseaseById(int diseaseId, CancellationToken cancellationToken)
        {
            return await GetByIdAsync(diseaseId, cancellationToken);
        }

        public async Task<(List<Disease>, int)> GetDiseasePaged(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await GetPagedAsync(pageNumber, pageSize, cancellationToken);
        }

        public async Task<int> CreateDisease(Disease disease, CancellationToken cancellationToken)
        {
            return await CreateAsync(disease, cancellationToken);
        }

        public async Task<int> UpdateDisease(Disease disease, CancellationToken cancellationToken)
        {
            return await UpdateAsync(disease, cancellationToken);
        }

        public async Task<bool> DeleteDisease(int diseaseId, CancellationToken cancellationToken)
        {
            return await DeleteAsync(diseaseId, cancellationToken);
        }
    }
}
