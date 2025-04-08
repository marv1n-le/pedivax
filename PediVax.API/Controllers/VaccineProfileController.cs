using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PediVax.BusinessObjects.DTO.VaccineDTO;
using PediVax.BusinessObjects.DTO.VaccineProfileDTO;
using PediVax.Services.IService;
using PediVax.Services.Service;
using System.Net;

namespace PediVax.Controllers
{
    [Route("api/vaccine-profile")]
    [ApiController]
    public class VaccineProfileController : ControllerBase
    {
        private readonly IVaccineProfileService _vaccineProfileService;
        private readonly ILogger<VaccineProfileController> _logger;

        public VaccineProfileController(IVaccineProfileService vaccineProfileService, ILogger<VaccineProfileController> logger)
        {
            _vaccineProfileService = vaccineProfileService;
            _logger = logger;
        }

        [HttpGet("get-all")]
        [ProducesResponseType(typeof(List<VaccineProfileResponseDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllVaccineProfiles(CancellationToken cancellationToken)
        {
            var response = await _vaccineProfileService.GetAllVaccineProfiles(cancellationToken);
            if (response == null || response.Count == 0)
            {
                _logger.LogWarning("No vaccine profiles found.");
                return NotFound(new { message = "No vaccine profiles available." });
            }

            return Ok(response);
        }

        [HttpGet("get-by-id/{vaccineProfileId}")]
        [ProducesResponseType(typeof(VaccineProfileResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetVaccineProfileById(int vaccineProfileId, CancellationToken cancellationToken)
        {
            if (vaccineProfileId <= 0)
            {
                return BadRequest(new { message = "Invalid vaccine profile ID." });
            }

            try
            {
                var response = await _vaccineProfileService.GetVaccineProfileById(vaccineProfileId, cancellationToken);
                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Vaccine profile not found." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching vaccine profile.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while fetching vaccine profile." });
            }
        }

        [HttpGet("get-by-child-id/{childId}")]
        [ProducesResponseType(typeof(VaccineProfileResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetVaccineProfileByChildId(int childId, CancellationToken cancellationToken)
        {
            if (childId <= 0)
            {
                return BadRequest(new { message = "Invalid child ID." });
            }

            try
            {
                var response = await _vaccineProfileService.GetVaccineProfileByChildId(childId, cancellationToken);
                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Vaccine profile not found." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching vaccine profile.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while fetching vaccine profile." });
            }
        }

        [HttpPost("generate-vaccine-profile/{childId}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateVaccineProfile(int childId, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data.", errors = ModelState.Values });
            }

            try
            {
                var response = await _vaccineProfileService.GenerateVaccineProfile(childId, cancellationToken);
                return CreatedAtAction(nameof(CreateVaccineProfile), new { childId = response }, response);
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error creating a new vaccine profile.");
                return Problem("An error occurred while creating the vaccine profile.");
            }
        }

        [HttpPut("update/{vaccineProfileId}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateVaccineProfile(int vaccineProfileId, [FromBody] UpdateVaccineProfileDTO request, CancellationToken cancellationToken)
        {
            if (vaccineProfileId <= 0)
            {
                return BadRequest(new { message = "Invalid vaccine profile ID." });
            }

            try
            {
                var response = await _vaccineProfileService.UpdateVaccineProfile(vaccineProfileId, request, cancellationToken);
                return Ok(new { success = response });
            }
            catch (KeyNotFoundException ex)
            {
                if (ex.Message.Contains("Vaccine profile not found"))
                {
                    return NotFound(new { message = "Vaccine profile not found." });
                }
                if (ex.Message.Contains("Appointment not found"))
                {
                    return NotFound(new { message = "Appointment not found." });
                }
                return NotFound(new { message = "Resource not found." });
            }
            catch (ArgumentException ex)
            {
                // Trường hợp appointment chưa hoàn thành
                return BadRequest(new { message = ex.Message });
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error updating vaccine profile with ID {vaccineProfileId}", vaccineProfileId);
                return Problem("An error occurred while updating the vaccine profile.");
            }
        }


        [HttpDelete("delete/{vaccineProfileId}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteVaccineProfile(int vaccineProfileId, CancellationToken cancellationToken)
        {
            if (vaccineProfileId <= 0)
            {
                return BadRequest(new { message = "Invalid vaccine profile ID." });
            }

            try
            {
                var response = await _vaccineProfileService.DeleteVaccineProfile(vaccineProfileId, cancellationToken);
                return Ok(new { success = response });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Vaccine profile not found." });
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error deleting vaccine profile with ID {vaccineProfileId}", vaccineProfileId);
                return Problem("An error occurred while deleting the vaccine profile.");
            }
        }
    }
}
