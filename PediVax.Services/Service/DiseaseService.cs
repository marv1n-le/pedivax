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
            var diseas = _mapper.Map<Disease>(createDiseaseDTO);
            return await _diseaseRepository.AddDisease(diseas);
        }

        public async Task<List<Disease>> GetAllDisease()
        {
            var diease = await _diseaseRepository.GetAllDisease();
            return _mapper.Map<List<Disease>>(diease);
        }
    }
}
