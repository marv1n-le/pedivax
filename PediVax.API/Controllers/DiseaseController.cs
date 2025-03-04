using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PediVax.BusinessObjects.DTO.DiseaseDTO;
using PediVax.Services.IService;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PediVax.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiseaseController : ControllerBase
    {
        private readonly IDiseaseService _diseaseService;
        private readonly ILogger<DiseaseController> _logger;

        public DiseaseController(IDiseaseService diseaseService, ILogger<DiseaseController> logger)
        {
            _diseaseService = diseaseService;
            _logger = logger;
        }

        [HttpGet("get-all")]
        [ProducesResponseType(typeof(List<DiseaseResponseDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllDisease(CancellationToken cancellationToken)
        {
            var response = await _diseaseService.GetAllDiseases(cancellationToken);
            if (response == null || response.Count == 0)
            {
                _logger.LogWarning("No diseases found.");
                return NotFound(new { message = "No diseases available." });
            }
            return Ok(response);
        }

        [HttpGet("get-by-id/{diseaseId}")]
        [ProducesResponseType(typeof(DiseaseResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetDiseaseById(int diseaseId, CancellationToken cancellationToken)
        {
            if (diseaseId <= 0)
            {
                return BadRequest(new { message = "Invalid disease ID." });
            }

            try
            {
                var response = await _diseaseService.GetDiseaseById(diseaseId, cancellationToken);
                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Disease not found." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching disease with ID {diseaseId}", diseaseId);
                return Problem("An unexpected error occurred.");
            }
        }

        [HttpGet("get-disease-paged/{pageNumber}/{pageSize}")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetDiseasePaged(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest(new { message = "Invalid pagination parameters." });
            }

            var (data, totalCount) = await _diseaseService.GetDiseasePaged(pageNumber, pageSize, cancellationToken);
            return Ok(new { Data = data, TotalCount = totalCount });
        }

        [HttpPost("create-disease")]
        [ProducesResponseType(typeof(DiseaseResponseDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddDisease([FromForm] CreateDiseaseDTO createDiseaseDTO, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data.", errors = ModelState.Values });
            }

            try
            {
                var response = await _diseaseService.AddDisease(createDiseaseDTO, cancellationToken);
                return CreatedAtAction(nameof(GetDiseaseById), new { diseaseId = response.DiseaseId }, response);
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error creating a new disease.");
                return Problem("An error occurred while creating the disease.");
            }
        }

        [HttpPut("update-disease/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateDisease(int id, [FromForm] UpdateDiseaseDTO updateDiseaseDTO, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid disease ID." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data.", errors = ModelState.Values });
            }

            try
            {
                var response = await _diseaseService.UpdateDisease(id, updateDiseaseDTO, cancellationToken);
                return Ok(new { success = response });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Disease not found." });
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error updating disease with ID {id}", id);
                return Problem("An error occurred while updating the disease.");
            }
        }

        [HttpDelete("delete-disease/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteDisease(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid disease ID." });
            }

            try
            {
                var response = await _diseaseService.DeleteDisease(id, cancellationToken);
                if (!response)
                {
                    return NotFound(new { message = "Disease not found or already deleted." });
                }
                return Ok(new { message = "Disease deleted successfully." });
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error deleting disease with ID {id}", id);
                return Problem("An error occurred while deleting the disease.");
            }
        }
    }
}
