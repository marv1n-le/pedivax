using PediVax.BusinessObjects.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PediVax.Repositories.IRepository
{
    public interface IVaccineDiseaseRepository
    {
        Task<List<VaccineDisease>> GetAllVaccineDiseases(CancellationToken cancellationToken);
        Task<VaccineDisease?> GetVaccineDiseaseById(int vaccineDiseaseId, CancellationToken cancellationToken);
        Task<(List<VaccineDisease>, int)> GetVaccineDiseasePaged(int pageNumber, int pageSize, CancellationToken cancellationToken);

        Task<int> CreateVaccineDisease(VaccineDisease vaccineDisease, CancellationToken cancellationToken);
        Task<int> UpdateVaccineDisease(VaccineDisease vaccineDisease, CancellationToken cancellationToken);
        Task<bool> DeleteVaccineDisease(int vaccineDiseaseId, CancellationToken cancellationToken);
    }
}
