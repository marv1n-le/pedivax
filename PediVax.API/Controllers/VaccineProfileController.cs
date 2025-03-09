using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PediVax.BusinessObjects.DTO.VaccineDTO;
using PediVax.BusinessObjects.DTO.VaccineProfileDTO;
using PediVax.Services.IService;
using PediVax.Services.Service;
using System.Net;

namespace PediVax.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPost("create")]
        [ProducesResponseType(typeof(VaccineProfileResponseDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateVaccineProfile([FromForm] CreateVaccineProfileDTO request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data.", errors = ModelState.Values });
            }

            try
            {
                var response = await _vaccineProfileService.AddVaccineProfile(request, cancellationToken);
                return CreatedAtAction(nameof(GetVaccineProfileById), new { vaccineProfileId = response.VaccineProfileId }, response);
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error creating a new vaccine profile.");
                return Problem("An error occurred while creating the vaccine profile.");
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
