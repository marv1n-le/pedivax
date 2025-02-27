using AutoMapper;
using PediVax.BusinessObjects.DTO.RequestDTO;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Repositories.Repository;
using PediVax.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.Services.Service
{
    public class DiseaseService : IDiseaseService
    {
        private readonly IDiseaseRepository _diseaseRepository;
        private readonly IMapper _mapper;

        public DiseaseService(IDiseaseRepository diseaseRepository, IMapper mapper)
        {
            _diseaseRepository = diseaseRepository;
            _mapper = mapper;
        }

        public async Task<Disease> AddDisease(CreateDiseaseDTO createDiseaseDTO)
        {
            var disease = _mapper.Map<Disease>(createDiseaseDTO);
            disease.IsActive = bool.TryParse(createDiseaseDTO.IsActive, out bool isActive) ? isActive.ToString() : "false";
            disease.CreatedDate = DateTime.UtcNow;
            disease.ModifiedDate = DateTime.UtcNow;
            var createDisease = await _diseaseRepository.AddDisease(disease);
            return disease;
        }

        public async Task<List<Disease>> GetAllDisease()
        {
            var disease = await _diseaseRepository.GetAllDisease();
            return _mapper.Map<List<Disease>>(disease);
        }
    }
}
