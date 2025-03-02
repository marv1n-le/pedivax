using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PediVax.BusinessObjects.Enum;
using PediVax.BusinessObjects.DTO.AppointmentDTO;

namespace PediVax.Services.IService
{
    public interface IAppointmentService
    {
        Task<List<AppointmentResponseDTO>> GetAllAppointments();
        Task<AppointmentResponseDTO> GetAppointmentById(int appointmentId);
        Task<(List<AppointmentResponseDTO> Data, int TotalCount)> GetAppointmentsPaged(int pageNumber, int pageSize);
        Task<AppointmentResponseDTO> CreateAppointment(CreateAppointmentDTO createAppointmentDTO);
        Task<bool> UpdateAppointment(int id, UpdateAppointmentDTO updateAppointmentDTO);
        Task<bool> DeleteAppointment(int appointmentId);
        Task<List<AppointmentResponseDTO>> GetAppointmentsByChildId(int childId);
        Task<List<AppointmentResponseDTO>> GetAppointmentsByDate(DateTime appointmentDate);
        Task<List<AppointmentResponseDTO>> GetAppointmentsByStatus(EnumList.AppointmentStatus status);
    }
}
