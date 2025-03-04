using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PediVax.Services.IService;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using PediVax.BusinessObjects.DTO.VaccinationRecordDTo;

namespace PediVax.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaccinationRecordController : ControllerBase
    {
        private readonly IVaccinationRecordService _vaccinationRecordService;
        private readonly ILogger<VaccinationRecordController> _logger;

        public VaccinationRecordController(IVaccinationRecordService vaccinationRecordService, ILogger<VaccinationRecordController> logger)
        {
            _vaccinationRecordService = vaccinationRecordService;
            _logger = logger;
        }

        [HttpGet("get-all")]
        [ProducesResponseType(typeof(List<VaccinationRecordRequestDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllVaccinationRecords(CancellationToken cancellationToken)
        {
            var response = await _vaccinationRecordService.GetAllVaccinationRecords(cancellationToken);
            if (response == null || response.Count == 0)
            {
                _logger.LogWarning("No vaccination records found.");
                return NotFound(new { message = "No vaccination records available." });
            }

            return Ok(response);
        }

        [HttpGet("get-by-id/{recordId}")]
        [ProducesResponseType(typeof(VaccinationRecordRequestDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetVaccinationRecordById(int recordId, CancellationToken cancellationToken)
        {
            if (recordId <= 0)
            {
                return BadRequest(new { message = "Invalid vaccination record ID." });
            }

            try
            {
                var response = await _vaccinationRecordService.GetVaccinationRecordById(recordId, cancellationToken);
                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Vaccination record not found." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching vaccination record with ID {recordId}", recordId);
                return Problem("An unexpected error occurred.");
            }
        }

        [HttpGet("get-vaccination-records-paged/{pageNumber}/{pageSize}")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetVaccinationRecordsPaged(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest(new { message = "Invalid pagination parameters." });
            }

            var (data, totalCount) = await _vaccinationRecordService.GetVaccinationRecordsPaged(pageNumber, pageSize, cancellationToken);
            return Ok(new { Data = data, TotalCount = totalCount });
        }

        [HttpPost("create-vaccination-record")]
        [ProducesResponseType(typeof(VaccinationRecordRequestDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddVaccinationRecord([FromForm] CreateVaccinationRecordDTO createVaccinationRecordDTO, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data.", errors = ModelState.Values });
            }

            try
            {
                var response = await _vaccinationRecordService.AddVaccinationRecord(createVaccinationRecordDTO, cancellationToken);
                return CreatedAtAction(nameof(GetVaccinationRecordById), new { recordId = response }, response);
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error creating a new vaccination record.");
                return Problem("An error occurred while creating the vaccination record.");
            }
        }

        [HttpPut("update-vaccination-record/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateVaccinationRecord(int id, [FromForm] UpdateVaccinationRecordDTO updateVaccinationRecordDTO, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid vaccination record ID." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data.", errors = ModelState.Values });
            }

            try
            {
                var response = await _vaccinationRecordService.UpdateVaccinationRecord(id, updateVaccinationRecordDTO, cancellationToken);
                return Ok(new { success = response });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Vaccination record not found." });
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error updating vaccination record with ID {id}", id);
                return Problem("An error occurred while updating the vaccination record.");
            }
        }

        [HttpDelete("delete-vaccination-record/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteVaccinationRecord(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid vaccination record ID." });
            }

            try
            {
                var response = await _vaccinationRecordService.DeleteVaccinationRecord(id, cancellationToken);
                if (!response)
                {
                    return NotFound(new { message = "Vaccination record not found or already deleted." });
                }

                return Ok(new { message = "Vaccination record deleted successfully." });
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error deleting vaccination record with ID {id}", id);
                return Problem("An error occurred while deleting the vaccination record.");
            }
        }
    }
}
