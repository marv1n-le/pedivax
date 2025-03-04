using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Repositories.Repository.BaseRepository;
using System.Threading;

namespace PediVax.Repositories.Repository
{
    public class VaccinationRecordRepository : GenericRepository<VaccinationRecord>, IVaccinationRecordRepository
    {
        public VaccinationRecordRepository() : base()
        {
        }

        public async Task<List<VaccinationRecord>> GetAllVaccinationRecords(CancellationToken cancellationToken)
        {
            return await GetAllAsync(cancellationToken);
        }

        public async Task<(List<VaccinationRecord> Data, int TotalCount)> GetVaccinationRecordsPaged(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await GetPagedAsync(pageNumber, pageSize, cancellationToken);
        }

        public async Task<VaccinationRecord> GetVaccinationRecordById(int id, CancellationToken cancellationToken)
        {
            return await GetByIdAsync(id, cancellationToken);
        }

        public async Task<int> AddVaccinationRecord(VaccinationRecord vaccinationRecord, CancellationToken cancellationToken)
        {
            return await CreateAsync(vaccinationRecord, cancellationToken);
        }

        public async Task<int> UpdateVaccinationRecord(VaccinationRecord vaccinationRecord, CancellationToken cancellationToken)
        {
            return await UpdateAsync(vaccinationRecord, cancellationToken);
        }

        public async Task<bool> DeleteVaccinationRecord(int id, CancellationToken cancellationToken)
        {
            return await DeleteAsync(id, cancellationToken);
        }
    }
}
