using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PediVax.BusinessObjects.DTO.RequestDTO;
using PediVax.Services.IService;

namespace PediVax.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseaseController : ControllerBase
    {
        private readonly IDiseaseService _diseaseService;

        public DiseaseController(IDiseaseService diseaseService)
        {
            _diseaseService = diseaseService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var diseases = await _diseaseService.GetAllDisease();
            if (diseases == null || diseases.Count == 0)
            {
                return NotFound("No diseases found");
            }
            return Ok(diseases);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDisease([FromBody] CreateDiseaseDTO createDiseaseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var disease = await _diseaseService.AddDisease(createDiseaseDTO);
            return Ok(disease);
        }
    }
}
