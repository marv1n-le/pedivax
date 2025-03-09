using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PediVax.BusinessObjects.DTO.VaccineSchedulePersonalDTO;
using PediVax.Services.IService;
using System.Net;

namespace PediVax.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineSchedulePersonalController : ControllerBase
    {
        private readonly IVaccineSchedulePersonalService _vaccineSchedulePersonalService;
        private readonly ILogger<VaccineSchedulePersonalController> _logger;

        public VaccineSchedulePersonalController(IVaccineSchedulePersonalService vaccineSchedulePersonalService, ILogger<VaccineSchedulePersonalController> logger)
        {
            _vaccineSchedulePersonalService = vaccineSchedulePersonalService;
            _logger = logger;
        }

        [HttpGet("get-all-vaccine-schedule-personal")]
        [ProducesResponseType(typeof(List<VaccineSchedulePersonalResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllVaccineSchedulePersonal(CancellationToken cancellationToken)
        {
            try
            {
                var response = await _vaccineSchedulePersonalService.GetAllVaccineSchedulePersonal(cancellationToken);
                if (response == null || response.Count == 0)
                {
                    _logger.LogWarning("No vaccine schedule personal found.");
                    return NotFound(new { message = "No vaccine schedule personal available." });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching vaccine schedule personal.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error occurred while fetching vaccine schedule personal." });
            }
        }

        [HttpGet("get-vaccine-schedule-personal-by-childId/{childId}")]
        [ProducesResponseType(typeof(VaccineSchedulePersonalResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetVaccineSchedulePersonalByChildId(int childId, CancellationToken cancellationToken)
        {
            if (childId <= 0)
            {
                return BadRequest(new { message = "Invalid child ID." });
            }

            try
            {
                var response = await _vaccineSchedulePersonalService.GetVaccineSchedulePersonalByChildId(childId, cancellationToken);
                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Vaccine schedule personal not found." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching vaccine schedule personal.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error occurred while fetching vaccine schedule personal." });
            }
        }

        [HttpPost("add-vaccine-schedule/{childId}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GenerateVaccineScheduleForChild(int childId, CancellationToken cancellationToken)
        {
            if (childId <= 0)
            {
                return BadRequest(new { message = "Invalid child ID." });
            }

            try
            {
                var response = await _vaccineSchedulePersonalService.GenerateVaccineScheduleForChild(childId, cancellationToken);
                return CreatedAtAction(nameof(GenerateVaccineScheduleForChild), new { childId = response }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding vaccine schedule.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error occurred while adding vaccine schedule." });
            }
        }

        [HttpDelete("delete-vaccine-schedule-personal/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteVaccineSchedulePersonal(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid vaccine schedule personal ID." });
            }

            try
            {
                var response = await _vaccineSchedulePersonalService.DeleteVaccineSchedulePersonal(id, cancellationToken);
                return Ok(new { success = response });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Vaccine schedule personal not found." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting vaccine schedule personal with ID {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error occurred while deleting vaccine schedule personal." });
            }
        }   
    }
}
