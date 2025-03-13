using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PediVax.BusinessObjects.DTO.VaccineDTO;
using PediVax.Services.IService;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using PediVax.BusinessObjects.DTO.VaccinePackageDTO;

namespace PediVax.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaccinePackageController : ControllerBase
    {
        private readonly IVaccinePackageService _vaccinePackageService;
        private readonly ILogger<VaccinePackageController> _logger;

        public VaccinePackageController(IVaccinePackageService vaccinePackageService, ILogger<VaccinePackageController> logger)
        {
            _vaccinePackageService = vaccinePackageService;
            _logger = logger;
        }

        [HttpGet("get-all")]
        [ProducesResponseType(typeof(List<VaccinePackageResponseDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllVaccinePackages(CancellationToken cancellationToken)
        {
            var response = await _vaccinePackageService.GetAllVaccinePackages(cancellationToken);
            if (response == null || response.Count == 0)
            {
                _logger.LogWarning("No vaccine packages found.");
                return NotFound(new { message = "No vaccine packages available." });
            }
            return Ok(response);
        }

        [HttpGet("get-by-id/{vaccinePackageId}")]
        [ProducesResponseType(typeof(VaccinePackageResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetVaccinePackageById(int vaccinePackageId, CancellationToken cancellationToken)
        {
            if (vaccinePackageId <= 0)
            {
                return BadRequest(new { message = "Invalid package ID." });
            }

            try
            {
                var response = await _vaccinePackageService.GetVaccinePackageById(vaccinePackageId, cancellationToken);
                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Vaccine package not found." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching vaccine package with ID {packageId}", vaccinePackageId);
                return Problem("An unexpected error occurred.");
            }
        }

        [HttpPost("create-vaccine-package")]
        [ProducesResponseType(typeof(VaccinePackageResponseDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddVaccinePackage([FromBody] CreateVaccinePackageDTO createVaccinePackageDTO, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data.", errors = ModelState.Values });
            }

            try
            {
                var response = await _vaccinePackageService.AddVaccinePackage(createVaccinePackageDTO, cancellationToken);
                return CreatedAtAction(nameof(GetVaccinePackageById), new { vaccinePackageId = response.VaccinePackageId }, response);
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error creating a new vaccine package.");
                return Problem("An error occurred while creating the vaccine package.");
            }
        }

        [HttpPut("update-vaccine-package/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateVaccinePackage(int id, [FromBody] UpdateVaccinePackageDTO updateVaccinePackageDTO, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid package ID." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data.", errors = ModelState.Values });
            }

            try
            {
                var response = await _vaccinePackageService.UpdateVaccinePackage(id, updateVaccinePackageDTO, cancellationToken);
                return Ok(new { success = response });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Vaccine package not found." });
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error updating vaccine package with ID {id}", id);
                return Problem("An error occurred while updating the vaccine package.");
            }
        }

        [HttpPost("update-total-price/{vaccinePackageId}")]
        public async Task<IActionResult> UpdateTotalPrice(int vaccinePackageId, CancellationToken cancellationToken)
        {
            try
            {
                bool result = await _vaccinePackageService.UpdateTotalPrice(vaccinePackageId, cancellationToken);

                if (result)
                {
                    return Ok(new { Message = "TotalPrice updated successfully" });
                }
                else
                {
                    return BadRequest(new { Message = "Failed to update TotalPrice" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating TotalPrice for VaccinePackage with ID {packageId}", vaccinePackageId);
                return StatusCode(500, new { Message = "An error occurred while updating TotalPrice" });
            }
        }


        [HttpDelete("delete-vaccine-package/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteVaccinePackage(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid package ID." });
            }

            try
            {
                var response = await _vaccinePackageService.DeleteVaccinePackage(id, cancellationToken);
                if (!response)
                {
                    return NotFound(new { message = "Vaccine package not found or already deleted." });
                }
                return Ok(new { message = "Vaccine package deleted successfully." });
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error deleting vaccine package with ID {id}", id);
                return Problem("An error occurred while deleting the vaccine package.");
            }
        }
    }
}
