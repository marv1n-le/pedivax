using PediVax.BusinessObjects.DTO.RequestDTO;
using PediVax.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.Services.IService
{
    public interface IDiseaseService
    {
        Task<List<Disease>> GetAllDisease();
        Task<Disease> AddDisease(CreateDiseaseDTO createDiseaseDTO);
    }
}
