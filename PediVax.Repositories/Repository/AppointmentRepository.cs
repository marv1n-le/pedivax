using Microsoft.EntityFrameworkCore;
using PediVax.BusinessObjects.DBContext;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PediVax.Repositories.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly PediVaxContext _context;

        public AppointmentRepository()
        {
            _context ??= new PediVaxContext();
        }

        // Lấy tất cả cuộc hẹn
        public async Task<List<Appointment>> GetAllAppointments()
        {
            return await _context.Appointments.ToListAsync();
        }

        // Lấy cuộc hẹn theo ID
        public async Task<Appointment> GetAppointmentById(int appointmentId)
        {
            return await _context.Appointments.FindAsync(appointmentId);
        }

        // Lấy danh sách cuộc hẹn của một trẻ em
        public async Task<List<Appointment>> GetAppointmentsByChildId(int childId)
        {
            return await _context.Appointments.Where(a => a.ChildId == childId).ToListAsync();
        }

        // Lấy danh sách cuộc hẹn theo ngày
        public async Task<List<Appointment>> GetAppointmentsByDate(DateTime appointmentDate)
        {
            return await _context.Appointments
                .Where(a => a.AppointmentDate.Date == appointmentDate.Date)
                .ToListAsync();
        }


        // Thêm mới cuộc hẹn
        public async Task<Appointment> AddAppointment(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        // Cập nhật thông tin cuộc hẹn
        public async Task<Appointment> UpdateAppointment(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        // Xóa cuộc hẹn theo ID
        public async Task<bool> DeleteAppointment(int appointmentId)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment == null) return false;

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
