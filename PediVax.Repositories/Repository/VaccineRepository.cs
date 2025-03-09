using Microsoft.EntityFrameworkCore;
using PediVax.BusinessObjects.Enum;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Repositories.Repository.BaseRepository;
using System.Threading;

namespace PediVax.Repositories.Repository;

public class VaccineRepository : GenericRepository<Vaccine>, IVaccineRepository
{
    public VaccineRepository() : base()
    {
    }
    
    public async Task<List<Vaccine>> GetAllVaccine(CancellationToken cancellationToken)
    {
        return await GetAllAsync(cancellationToken);
    }
    
    public async Task<(List<Vaccine> Data, int TotalCount)> GetVaccinePaged(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await GetPagedAsync(pageNumber, pageSize, cancellationToken);
    }
    
    public async Task<Vaccine> GetVaccineById(int id, CancellationToken cancellationToken) 
    {
        return await GetByIdAsync(id, cancellationToken);
    }
    
    public async Task<List<Vaccine>> GetVaccineByName(string keyword, CancellationToken cancellationToken)
    {
        return await GetByNameContainingAsync(keyword, cancellationToken);
    }
    public async Task<int> AddVaccine(Vaccine vaccine, CancellationToken cancellationToken)
    {
        return await CreateAsync(vaccine, cancellationToken);
    }

    public async Task<int> UpdateVaccine(Vaccine vaccine, CancellationToken cancellationToken)
    {
        return await UpdateAsync(vaccine, cancellationToken);
    }
    
    public async Task<bool> DeleteVaccine(int id, CancellationToken cancellationToken)
    {
        return await DeleteAsync(id, cancellationToken);
    }
}