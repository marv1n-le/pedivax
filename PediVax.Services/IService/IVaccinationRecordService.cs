using PediVax.BusinessObjects.DTO;
using PediVax.BusinessObjects.DTO.VaccinationRecordDTo;
using PediVax.BusinessObjects.Models;
using System.Threading;

namespace PediVax.Services.IService
{
    public interface IVaccinationRecordService
    {
        Task<List<VaccinationRecordRequestDTO>> GetAllVaccinationRecords(CancellationToken cancellationToken);
        Task<VaccinationRecordRequestDTO> GetVaccinationRecordById(int recordId, CancellationToken cancellationToken);
        Task<(List<VaccinationRecordRequestDTO> Data, int TotalCount)> GetVaccinationRecordsPaged(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<int> AddVaccinationRecord(CreateVaccinationRecordDTO createVaccinationRecordDTO, CancellationToken cancellationToken);
        Task<bool> UpdateVaccinationRecord(int recordId, UpdateVaccinationRecordDTO updateVaccinationRecordDTO, CancellationToken cancellationToken);
        Task<bool> DeleteVaccinationRecord(int recordId, CancellationToken cancellationToken);
    }
}
