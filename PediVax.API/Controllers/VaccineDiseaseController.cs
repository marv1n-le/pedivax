using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PediVax.BusinessObjects.DTO.VaccineDiseaseDTO;
using PediVax.Services.IService;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PediVax.Controllers
{
    [ApiController]
    [Route("api/vaccine-disease")]
    public class VaccineDiseaseController : ControllerBase
    {
        private readonly IVaccineDiseaseService _vaccineDiseaseService;
        private readonly ILogger<VaccineDiseaseController> _logger;

        public VaccineDiseaseController(IVaccineDiseaseService vaccineDiseaseService, ILogger<VaccineDiseaseController> logger)
        {
            _vaccineDiseaseService = vaccineDiseaseService;
            _logger = logger;
        }

        [HttpGet("get-all")]
        [ProducesResponseType(typeof(List<VaccineDiseaseResponseDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllVaccineDiseases(CancellationToken cancellationToken)
        {
            var response = await _vaccineDiseaseService.GetAllVaccineDiseases(cancellationToken);
            if (response == null || response.Count == 0)
            {
                _logger.LogWarning("No vaccine-disease relationships found.");
                return NotFound(new { message = "No vaccine-disease relationships available." });
            }
            return Ok(response);
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(VaccineDiseaseResponseDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateVaccineDisease([FromBody] CreateVaccineDiseaseDTO createVaccineDiseaseDTO, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data.", errors = ModelState.Values });
            }

            try
            {
                var response = await _vaccineDiseaseService.AddVaccineDisease(createVaccineDiseaseDTO, cancellationToken);
                return CreatedAtAction(nameof(GetAllVaccineDiseases), new { id = response.VaccineDiseaseId }, response);
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error creating a new vaccine-disease relationship.");
                return Problem("An error occurred while creating the vaccine-disease relationship.");
            }
        }

        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateVaccineDisease(int id, [FromBody] UpdateVaccineDiseaseDTO updateVaccineDiseaseDTO, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid vaccine-disease ID." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data.", errors = ModelState.Values });
            }

            try
            {
                var response = await _vaccineDiseaseService.UpdateVaccineDisease(id, updateVaccineDiseaseDTO, cancellationToken);
                return Ok(new { success = response });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Vaccine-disease relationship not found." });
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error updating vaccine-disease relationship with ID {id}", id);
                return Problem("An error occurred while updating the vaccine-disease relationship.");
            }
        }
        [HttpDelete("delete/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteVaccineDisease(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid vaccine-disease ID." });
            }

            try
            {
                var isDeleted = await _vaccineDiseaseService.DeleteVaccineDisease(id, cancellationToken);
                if (!isDeleted)
                {
                    return NotFound(new { message = "Vaccine-disease relationship not found." });
                }

                return Ok(new { success = true, message = "Vaccine-disease relationship deleted successfully." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Vaccine-disease relationship not found." });
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error deleting vaccine-disease relationship with ID {id}", id);
                return Problem("An error occurred while deleting the vaccine-disease relationship.");
            }
        }

    }
}
