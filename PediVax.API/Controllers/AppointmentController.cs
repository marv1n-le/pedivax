using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PediVax.BusinessObjects.DTO.AppointmentDTO;
using PediVax.BusinessObjects.DTO.ResponseDTO;
using PediVax.BusinessObjects.Enum;
using PediVax.Services.IService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PediVax.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

       
        [HttpGet("GetAllAppointments")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await _appointmentService.GetAllAppointments();
            if (appointments == null || appointments.Count == 0)
            {
                return NotFound("No appointments found");
            }
            return Ok(appointments);
        }

        
        [HttpGet("GetAppointmentById/{appointmentId}")]
        public async Task<IActionResult> GetAppointmentById(int appointmentId)
        {
            var appointment = await _appointmentService.GetAppointmentById(appointmentId);
            if (appointment == null)
            {
                return NotFound("Appointment not found");
            }
            return Ok(appointment);
        }

        
        [HttpGet("GetAppointmentsPaged/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetAppointmentsPaged(int pageNumber, int pageSize)
        {
            var (appointments, totalCount) = await _appointmentService.GetAppointmentsPaged(pageNumber, pageSize);
            return Ok(new { Data = appointments, TotalCount = totalCount });
        }

        
        [HttpPost("CreateAppointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDTO createAppointmentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdAppointment = await _appointmentService.CreateAppointment(createAppointmentDTO);
            return Ok(createdAppointment);
        }

        
        [HttpPut("UpdateAppointmentById/{id}")]
        public async Task<IActionResult> UpdateAppointment([FromRoute] int id, [FromBody] UpdateAppointmentDTO updateAppointmentDTO)
        {
            var result = await _appointmentService.UpdateAppointment(id, updateAppointmentDTO);
            if (!result)
            {
                return NotFound("Appointment not found");
            }
            return NoContent();
        }

        
        [HttpDelete("DeleteAppointmentById/{appointmentId}")]
        public async Task<IActionResult> DeleteAppointment(int appointmentId)
        {
            var result = await _appointmentService.DeleteAppointment(appointmentId);
            if (!result)
            {
                return NotFound("Appointment not found");
            }
            return NoContent();
        }

        
        [HttpGet("GetAppointmentsByChildId/{childId}")]
        public async Task<IActionResult> GetAppointmentsByChildId(int childId)
        {
            var appointments = await _appointmentService.GetAppointmentsByChildId(childId);
            if (appointments == null || appointments.Count == 0)
            {
                return NotFound("No appointments found for this child");
            }
            return Ok(appointments);
        }

        
        [HttpGet("GetAppointmentsByDate/{appointmentDate}")]
        public async Task<IActionResult> GetAppointmentsByDate(DateTime appointmentDate)
        {
            var appointments = await _appointmentService.GetAppointmentsByDate(appointmentDate);
            if (appointments == null || appointments.Count == 0)
            {
                return NotFound("No appointments found on this date");
            }
            return Ok(appointments);
        }

        
        [HttpGet("GetAppointmentsByStatus/{status}")]
        public async Task<IActionResult> GetAppointmentsByStatus(EnumList.AppointmentStatus status)
        {
            var appointments = await _appointmentService.GetAppointmentsByStatus(status);
            if (appointments == null || appointments.Count == 0)
            {
                return NotFound("No appointments found with this status");
            }
            return Ok(appointments);
        }
    }
}
