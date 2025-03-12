 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PediVax.BusinessObjects.DTO.VaccineScheduleDTO;
using PediVax.Services.IService;
using System.Net;

namespace PediVax.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineScheduleController : ControllerBase
    {
        private readonly IVaccineScheduleService _vaccineScheduleService;
        private readonly ILogger<VaccineScheduleController> _logger;

        public VaccineScheduleController(IVaccineScheduleService vaccineScheduleService, ILogger<VaccineScheduleController> logger)
        {
            _vaccineScheduleService = vaccineScheduleService;
            _logger = logger;
        }

        [HttpGet("get-all")]
        [ProducesResponseType(typeof(List<VaccineScheduleResponseDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllVaccineSchedule(CancellationToken cancellationToken)
        {
            var response = await _vaccineScheduleService.GetAllVaccineSchedule(cancellationToken);
            if (response == null || response.Count == 0)
            {
                _logger.LogWarning("No vaccine schedules found.");
                return NotFound(new { message = "No vaccine schedules available." });
            }

            return Ok(response);
        }

        [HttpGet("get-by-id/{vaccineScheduleId}")]
        [ProducesResponseType(typeof(VaccineScheduleResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetVaccineScheduleById(int vaccineScheduleId, CancellationToken cancellationToken)
        {
            if (vaccineScheduleId <= 0)
            {
                return BadRequest(new { message = "Invalid vaccine schedule ID." });
            }

            try
            {
                var response = await _vaccineScheduleService.GetVaccineScheduleById(vaccineScheduleId, cancellationToken);
                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Vaccine schedule not found." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching vaccine schedule.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while fetching vaccine schedule." });
            }
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(VaccineScheduleResponseDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateVaccineSchedule([FromBody] CreateVaccineScheduleDTO vaccineScheduleRequestDTO, CancellationToken cancellationToken)
        {
            if (vaccineScheduleRequestDTO == null)
            {
                return BadRequest(new { message = "Vaccine schedule data is required." });
            }

            try
            {
                var response = await _vaccineScheduleService.CreateVaccineSchedule(vaccineScheduleRequestDTO, cancellationToken);
                return CreatedAtAction(nameof(GetVaccineScheduleById), new { vaccineScheduleId = response.VaccineScheduleId }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating vaccine schedule.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while creating vaccine schedule." });
            }
        }

        [HttpPut("update/{vaccineScheduleId}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateVaccineSchedule(int vaccineScheduleId, [FromBody] UpdateVaccineScheduleDTO updateVaccineScheduleDTO, CancellationToken cancellationToken)
        {
            if (vaccineScheduleId <= 0)
            {
                return BadRequest(new { message = "Invalid vaccine schedule ID." });
            }

            try
            {
                var response = await _vaccineScheduleService.UpdateVaccineSchedule(vaccineScheduleId, updateVaccineScheduleDTO, cancellationToken);
                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Vaccine schedule not found." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating vaccine schedule.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while updating vaccine schedule." });
            }
        }

        [HttpDelete("delete/{vaccineScheduleId}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteVaccineSchedule(int vaccineScheduleId, CancellationToken cancellationToken)
        {
            if (vaccineScheduleId <= 0)
            {
                return BadRequest(new { message = "Invalid vaccine schedule ID." });
            }

            try
            {
                var response = await _vaccineScheduleService.DeleteVaccineSchedule(vaccineScheduleId, cancellationToken);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting vaccine schedule.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while deleting vaccine schedule." });
            }
        }
    }
}
