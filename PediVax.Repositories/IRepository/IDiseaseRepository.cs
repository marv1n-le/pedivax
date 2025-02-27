using PediVax.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.Repositories.IRepository
{
    public interface IDiseaseRepository
    {
        Task<List<Disease>> GetAllDisease();
        Task<Disease> AddDisease(Disease disease);
    }
}
