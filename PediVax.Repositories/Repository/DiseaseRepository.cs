using Microsoft.EntityFrameworkCore;
using PediVax.BusinessObjects.DBContext;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.Repositories.Repository
{
    public class DiseaseRepository : IDiseaseRepository
    {
        private readonly PediVaxContext _context;

        public DiseaseRepository() 
        { 
            _context = new PediVaxContext();
        }
        public DiseaseRepository(PediVaxContext context)
        {
            _context = context;
        }

        public async Task<Disease> AddDisease(Disease disease)
        {
            _context.Diseases.Add(disease);
            await _context.SaveChangesAsync();
            return disease;
        }

        public async Task<List<Disease>> GetAllDisease()
        {
            return await _context.Diseases.ToListAsync();
        }

    }
}
