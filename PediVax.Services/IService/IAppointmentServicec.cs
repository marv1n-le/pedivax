using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PediVax.BusinessObjects.Enum;
using PediVax.BusinessObjects.DTO.AppointmentDTO;

namespace PediVax.Services.IService
{
    public interface IAppointmentService
    {
        Task<List<AppointmentResponseDTO>> GetAllAppointments(CancellationToken cancellationToken);
        Task<AppointmentResponseDTO> GetAppointmentById(int appointmentId, CancellationToken cancellationToken);
        Task<(List<AppointmentResponseDTO> Data, int TotalCount)> GetAppointmentsPaged(int pageNumber, int pageSize, CancellationToken cancellationToken);
        //Task<AppointmentResponseDTO> CreateAppointment(CreateAppointmentDTO createAppointmentDTO, CancellationToken cancellationToken);
        Task<bool> UpdateAppointment(int id, UpdateAppointmentDTO updateAppointmentDTO, CancellationToken cancellationToken);
        Task<bool> DeleteAppointment(int appointmentId, CancellationToken cancellationToken);
        Task<List<AppointmentResponseDTO>> GetAppointmentsByChildId(int childId, CancellationToken cancellationToken);
        Task<List<AppointmentResponseDTO>> GetAppointmentsByDate(DateTime appointmentDate, CancellationToken cancellationToken);
        Task<List<AppointmentResponseDTO>> GetAppointmentsByStatus(EnumList.AppointmentStatus appointmentStatus, CancellationToken cancellationToken);
        Task<bool> UpdateAppointmentStatus(int appointmentId, EnumList.AppointmentStatus appointmentStatus, CancellationToken cancellationToken);
    }
}