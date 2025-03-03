using PediVax.BusinessObjects.Enum;
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

        Task<(List<Appointment> Data, int TotalCount)> GetAppointmentsPaged(int pageNumber, int pageSize);

        // Lấy cuộc hẹn theo ID
        Task<Appointment> GetAppointmentById(int appointmentId);

        // Lấy danh sách cuộc hẹn của một trẻ em
        Task<List<Appointment>> GetAppointmentsByChildId(int childId);

        // Lấy danh sách cuộc hẹn theo ngày
        Task<List<Appointment>> GetAppointmentsByDate(DateTime appointmentDate);

        // Lấy danh sách cuộc hẹn theo trạng thái (sử dụng Enum thay vì string)
        Task<List<Appointment>> GetAppointmentsByStatus(EnumList.AppointmentStatus status);

        // Thêm mới cuộc hẹn, trả về số lượng bản ghi bị ảnh hưởng
        Task<int> AddAppointment(Appointment appointment);

        // Cập nhật thông tin cuộc hẹn, trả về số lượng bản ghi bị ảnh hưởng
        Task<int> UpdateAppointment(Appointment appointment);

        // Xóa cuộc hẹn theo ID, trả về true nếu thành công
        Task<bool> DeleteAppointment(int appointmentId);
    }
}
