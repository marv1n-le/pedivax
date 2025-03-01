using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PediVax.BusinessObjects.DTO.RequestDTO;
using PediVax.Services.IService;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var appointments = await _appointmentService.GetAllAppointments();
            if (appointments == null || appointments.Count == 0)
            {
                return NotFound("No appointments found");
            }
            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var appointment = await _appointmentService.GetAppointmentById(id);
            if (appointment == null)
            {
                return NotFound($"No appointment found with ID {id}");
            }
            return Ok(appointment);
        }
        [HttpGet("child/{childId}")]
        public async Task<IActionResult> GetByChildId(int childId)
        {
            var appointments = await _appointmentService.GetAppointmentsByChildId(childId);
            if (appointments == null || appointments.Count == 0)
            {
                return NotFound($"No appointments found for child ID {childId}");
            }
            return Ok(appointments);
        }

        [HttpGet("date/{date}")]
        public async Task<IActionResult> GetByDate(DateTime date)
        {
            var appointments = await _appointmentService.GetAppointmentsByDate(date);
            if (appointments == null || appointments.Count == 0)
            {
                return NotFound($"No appointments found on {date:yyyy-MM-dd}");
            }
            return Ok(appointments);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetByStatus(string status)
        {
            var appointments = await _appointmentService.GetAppointmentsByStatus(status);
            if (appointments == null || appointments.Count == 0)
            {
                return NotFound($"No appointments found with status {status}");
            }
            return Ok(appointments);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDTO createAppointmentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appointment = await _appointmentService.AddAppointment(createAppointmentDTO);
            return Ok(appointment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] CreateAppointmentDTO createAppointmentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appointment = await _appointmentService.UpdateAppointment(createAppointmentDTO);
            return Ok(appointment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var result = await _appointmentService.DeleteAppointment(id);
            if (!result)
            {
                return NotFound($"No appointment found with ID {id}");
            }
            return Ok("Appointment deleted successfully");
        }
    }
}
