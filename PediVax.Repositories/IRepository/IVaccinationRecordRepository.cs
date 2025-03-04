using PediVax.BusinessObjects.Models;
using System.Threading;

namespace PediVax.Repositories.IRepository
{
    public interface IVaccinationRecordRepository
    {
        Task<List<VaccinationRecord>> GetAllVaccinationRecords(CancellationToken cancellationToken);
        Task<(List<VaccinationRecord> Data, int TotalCount)> GetVaccinationRecordsPaged(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<VaccinationRecord> GetVaccinationRecordById(int id, CancellationToken cancellationToken);
        Task<int> AddVaccinationRecord(VaccinationRecord vaccinationRecord, CancellationToken cancellationToken);
        Task<int> UpdateVaccinationRecord(VaccinationRecord vaccinationRecord, CancellationToken cancellationToken);
        Task<bool> DeleteVaccinationRecord(int id, CancellationToken cancellationToken);
    }
}
