using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PediVax.BusinessObjects.DTO.VaccineDTO;
using PediVax.Services.IService;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PediVax.Controllers
{
    [ApiController]
    [Route("api/vaccine")]
    public class VaccineController : ControllerBase
    {
        private readonly IVaccineService _vaccineService;
        private readonly ILogger<VaccineController> _logger;

        public VaccineController(IVaccineService vaccineService, ILogger<VaccineController> logger)
        {
            _vaccineService = vaccineService;
            _logger = logger;
        }

        [HttpGet("get-all")]
        [ProducesResponseType(typeof(List<VaccineResponseDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllVaccine(CancellationToken cancellationToken)
        {
            var response = await _vaccineService.GetAllVaccine(cancellationToken);
            if (response == null || response.Count == 0)
            {
                _logger.LogWarning("No vaccines found.");
                return NotFound(new { message = "No vaccines available." });
            }

            return Ok(response);
        }

        [HttpGet("get-by-id/{vaccineId}")]
        [ProducesResponseType(typeof(VaccineResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetVaccineById(int vaccineId, CancellationToken cancellationToken)
        {
            if (vaccineId <= 0)
            {
                return BadRequest(new { message = "Invalid vaccine ID." });
            }

            try
            {
                var response = await _vaccineService.GetVaccineById(vaccineId, cancellationToken);
                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Vaccine not found." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching vaccine with ID {vaccineId}", vaccineId);
                return Problem("An unexpected error occurred.");
            }
        }

        [HttpGet("get-vaccine-paged/{pageNumber}/{pageSize}")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetVaccinePaged(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest(new { message = "Invalid pagination parameters." });
            }

            var (data, totalCount) = await _vaccineService.GetVaccinePaged(pageNumber, pageSize, cancellationToken);
            return Ok(new { Data = data, TotalCount = totalCount });
        }

        [HttpGet("get-by-name/{keyword}")]
        [ProducesResponseType(typeof(List<VaccineResponseDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetVaccineByName(string keyword, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new { message = "Keyword cannot be empty." });
            }

            var response = await _vaccineService.GetVaccineByName(keyword, cancellationToken);
            if (response == null || response.Count == 0)
            {
                return NotFound(new { message = "No vaccines found with the given name." });
            }

            return Ok(response);
        }

        [HttpPost("create-vaccine")]
        [ProducesResponseType(typeof(VaccineResponseDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddVaccine([FromForm] CreateVaccineDTO createVaccineDTO, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data.", errors = ModelState.Values });
            }

            try
            {
                var response = await _vaccineService.AddVaccine(createVaccineDTO, cancellationToken);
                return CreatedAtAction(nameof(GetVaccineById), new { vaccineId = response.VaccineId }, response);
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error creating a new vaccine.");
                return Problem("An error occurred while creating the vaccine.");
            }
        }

        [HttpPut("update-vaccine/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateVaccine(int id, [FromForm] UpdateVaccineDTO updateVaccineDTO, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid vaccine ID." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data.", errors = ModelState.Values });
            }

            try
            {
                var response = await _vaccineService.UpdateVaccine(id, updateVaccineDTO, cancellationToken);
                return Ok(new { success = response });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Vaccine not found." });
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error updating vaccine with ID {id}", id);
                return Problem("An error occurred while updating the vaccine.");
            }
        }

        [HttpDelete("delete-vaccine/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteVaccine(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid vaccine ID." });
            }

            try
            {
                var response = await _vaccineService.DeleteVaccine(id, cancellationToken);
                if (!response)
                {
                    return NotFound(new { message = "Vaccine not found or already deleted." });
                }

                return Ok(new { message = "Vaccine deleted successfully." });
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error deleting vaccine with ID {id}", id);
                return Problem("An error occurred while deleting the vaccine.");
            }
        }
    }
}