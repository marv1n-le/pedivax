using PediVax.BusinessObjects.Enum;
using PediVax.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PediVax.Repositories.IRepository
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAllAppointments(CancellationToken cancellationToken);
        Task<(List<Appointment> Data, int TotalCount)> GetAppointmentsPaged(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<Appointment> GetAppointmentById(int appointmentId, CancellationToken cancellationToken);
        Task<List<Appointment>> GetAppointmentsByChildId(int childId, CancellationToken cancellationToken);
        Task<List<Appointment>> GetAppointmentsByDate(DateTime appointmentDate, CancellationToken cancellationToken);
        Task<List<Appointment>> GetAppointmentsByStatus(EnumList.AppointmentStatus status, CancellationToken cancellationToken);
        Task<int> AddAppointment(Appointment appointment, CancellationToken cancellationToken);
        Task<int> UpdateAppointment(Appointment appointment, CancellationToken cancellationToken);
        Task<bool> DeleteAppointment(int appointmentId, CancellationToken cancellationToken);
    }
}