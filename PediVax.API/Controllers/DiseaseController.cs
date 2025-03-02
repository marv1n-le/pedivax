using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PediVax.BusinessObjects.DTO.DiseaseDTO;
using PediVax.BusinessObjects.DTO.RequestDTO;
using PediVax.Services.IService;
using System.Threading.Tasks;

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

        [HttpGet("get-all-diseases")]
        public async Task<IActionResult> GetAllDiseases()
        {
            var diseases = await _diseaseService.GetAllDisease();
            if (diseases == null || diseases.Count == 0)
            {
                return NotFound("No diseases found");
            }
            return Ok(diseases);
        }

        [HttpGet("get-disease-by-id/{id}")]
        public async Task<IActionResult> GetDiseaseById(int id)
        {
            var disease = await _diseaseService.GetDiseasebyId(id);
            if (disease == null)
            {
                return NotFound("Disease not found");
            }
            return Ok(disease);
        }

        [HttpPost("create-disease")]
        //[Authorize(Roles = "Admin, Staff, Doctor")]
        public async Task<IActionResult> CreateDisease([FromBody] CreateDiseaseDTO createDiseaseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var disease = await _diseaseService.AddDisease(createDiseaseDTO);
            return Ok(disease);
        }

        [HttpPut("update-disease-by-id/{id}")]
        [Authorize(Roles = "Admin, Staff, Doctor")]
        public async Task<IActionResult> UpdateDisease(int id, [FromBody] UpdateDiseaseDTO updateDiseaseDTO)
        {
            var result = await _diseaseService.UpdateDisease(id, updateDiseaseDTO);
            if (!result)
            {
                return NotFound("Disease not found");
            }
            return NoContent();
        }

        [HttpDelete("delete-disease-by-id/{id}")]
        [Authorize(Roles = "Admin, Staff, Doctor")]
        public async Task<IActionResult> DeleteDisease(int id)
        {
            var result = await _diseaseService.DeleteDisease(id);
            if (!result)
            {
                return NotFound("Disease not found");
            }
            return NoContent();
        }

        [HttpGet("get-disease-paged/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetDiseasesPaged(int pageNumber, int pageSize)
        {
            var (data, totalCount) = await _diseaseService.GetDiseasePaged(pageNumber, pageSize);
            return Ok(new { Data = data, TotalCount = totalCount });
        }
    }
}
