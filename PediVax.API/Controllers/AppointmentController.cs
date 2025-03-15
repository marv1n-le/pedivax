using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PediVax.BusinessObjects.DTO.AppointmentDTO;
using PediVax.BusinessObjects.Enum;
using PediVax.Services.IService;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace PediVax.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(IAppointmentService appointmentService, ILogger<AppointmentController> logger)
        {
            _appointmentService = appointmentService;
            _logger = logger;
        }

        [HttpPost("create")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDTO request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid request data." });
            }

            var createdAppointment = await _appointmentService.CreateAppointment(request, cancellationToken);
            if (createdAppointment == null)
            {
                return BadRequest(new { message = "Failed to create appointment." });
            }

            return CreatedAtAction(nameof(GetAppointmentById), new { appointmentId = createdAppointment.AppointmentId }, createdAppointment);
        }

        [HttpPut("update/{appointmentId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateAppointment(int appointmentId, [FromBody] UpdateAppointmentDTO request, CancellationToken cancellationToken)
        {
            if (appointmentId <= 0)
            {
                return BadRequest(new { message = "Invalid appointment ID." });
            }

            var existingAppointment = await _appointmentService.GetAppointmentById(appointmentId, cancellationToken);
            if (existingAppointment == null)
            {
                return NotFound(new { message = "Appointment not found." });
            }

            var updatedAppointment = await _appointmentService.UpdateAppointment(appointmentId, request, cancellationToken);
            return Ok(updatedAppointment);
        }


        [HttpGet("get-all")]
        [ProducesResponseType(typeof(List<AppointmentResponseDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllAppointments(CancellationToken cancellationToken)
        {
            var response = await _appointmentService.GetAllAppointments(cancellationToken);
            if (response == null || response.Count == 0)
            {
                _logger.LogWarning("No appointments found.");
                return NotFound(new { message = "No appointments available." });
            }
            return Ok(response);
        }

        [HttpGet("get-by-id/{appointmentId}")]
        [ProducesResponseType(typeof(AppointmentResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAppointmentById(int appointmentId, CancellationToken cancellationToken)
        {
            if (appointmentId <= 0)
            {
                return BadRequest(new { message = "Invalid appointment ID." });
            }
            try
            {
                var response = await _appointmentService.GetAppointmentById(appointmentId, cancellationToken);
                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Appointment not found." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching appointment with ID {appointmentId}", appointmentId);
                return Problem("An unexpected error occurred.");
            }
        }

        [HttpGet("get-by-status/{appointmentStatus}")]
        [ProducesResponseType(typeof(List<AppointmentResponseDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAppointmentsByStatus(EnumList.AppointmentStatus appointmentStatus, CancellationToken cancellationToken)
        {
            if (!Enum.IsDefined(typeof(EnumList.AppointmentStatus), appointmentStatus))
            {
                return BadRequest(new { message = "Invalid appointment status." });
            }
            var response = await _appointmentService.GetAppointmentsByStatus(appointmentStatus, cancellationToken);
            return Ok(response);
        }

        [HttpGet("get-by-childId/{childId}")]
        [ProducesResponseType(typeof(List<AppointmentResponseDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAppointmentsByChildId(int childId, CancellationToken cancellationToken)
        {
            if (childId <= 0)
            {
                return BadRequest(new { message = "Invalid child ID." });
            }
            var response = await _appointmentService.GetAppointmentsByChildId(childId, cancellationToken);
            return Ok(response);
        }

        [HttpGet("get-by-date/{appointmentDate}")]
        [ProducesResponseType(typeof(List<AppointmentResponseDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAppointmentsByDate(DateTime appointmentDate, CancellationToken cancellationToken)
        {
            if (appointmentDate == default)
            {
                return BadRequest(new { message = "Invalid appointment date." });
            }
            var response = await _appointmentService.GetAppointmentsByDate(appointmentDate, cancellationToken);
            return Ok(response);
        }
    }
}