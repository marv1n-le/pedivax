using PediVax.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PediVax.Repositories.IRepository
{
    public interface IAppointmentRepository
    {
        // Lấy tất cả cuộc hẹn
        Task<List<Appointment>> GetAllAppointments();

        // Lấy cuộc hẹn theo ID
        Task<Appointment> GetAppointmentById(int appointmentId);

        // Lấy danh sách cuộc hẹn của một trẻ em
        Task<List<Appointment>> GetAppointmentsByChildId(int childId);

        // Lấy danh sách cuộc hẹn theo ngày
        Task<List<Appointment>> GetAppointmentsByDate(DateTime appointmentDate);

        // Lấy danh sách cuộc hẹn theo trạng thái 
        Task<List<Appointment>> GetAppointmentsByStatus(string status);

        // Thêm mới cuộc hẹn
        Task<Appointment> AddAppointment(Appointment appointment);

        // Cập nhật thông tin cuộc hẹn
        Task<Appointment> UpdateAppointment(Appointment appointment);

        // Xóa cuộc hẹn theo ID
        Task<bool> DeleteAppointment(int appointmentId);
    }
}
