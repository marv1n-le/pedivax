using Microsoft.EntityFrameworkCore;
using PediVax.BusinessObjects.DBContext;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Repositories.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PediVax.BusinessObjects.Enum;

namespace PediVax.Repositories.Repository
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository() : base()
        {
        }

        public async Task<List<Appointment>> GetAllAppointments()
        {
            return await GetAllAsync();
        }

        public async Task<Appointment> GetAppointmentById(int appointmentId)
        {
            return await GetByIdAsync(appointmentId);
        }

        public async Task<(List<Appointment> Data, int TotalCount)> GetAppointmentsPaged(int pageNumber, int pageSize)
        {
            return await GetPagedAsync(pageNumber, pageSize);
        }

        public async Task<int> AddAppointment(Appointment appointment)
        {
            return await CreateAsync(appointment);
        }

        public async Task<int> UpdateAppointment(Appointment appointment)
        {
            return await UpdateAsync(appointment);
        }

        public async Task<bool> DeleteAppointment(int appointmentId)
        {
            return await DeleteAsync(appointmentId);
        }

        public async Task<List<Appointment>> GetAppointmentsByChildId(int childId)
        {
            return await _context.Appointments
                .Where(a => a.ChildId == childId)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByDate(DateTime appointmentDate)
        {
            return await _context.Appointments
                .Where(a => a.AppointmentDate.Date == appointmentDate.Date)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByStatus(EnumList.AppointmentStatus status)
        {
            return await _context.Appointments
                .Where(a => a.AppointmentStatus == status)
                .ToListAsync();
        }
    }
}
