using PediVax.BusinessObjects.Models;
using System.Threading;

namespace PediVax.Repositories.IRepository;

public interface IVaccineRepository
{
    Task<List<Vaccine>> GetAllVaccine(CancellationToken cancellationToken);
    Task<(List<Vaccine> Data, int TotalCount)> GetVaccinePaged(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<Vaccine> GetVaccineById(int id, CancellationToken cancellationToken);
    Task<List<Vaccine>> GetVaccineByName(string keyword, CancellationToken cancellationToken);
    Task<int> AddVaccine(Vaccine vaccine, CancellationToken cancellationToken);
    Task<int> UpdateVaccine(Vaccine vaccine, CancellationToken cancellationToken);
    Task<bool> DeleteVaccine(int id, CancellationToken cancellationToken);
}