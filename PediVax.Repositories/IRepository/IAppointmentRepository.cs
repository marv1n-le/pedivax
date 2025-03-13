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
        // Lấy tất cả cuộc hẹn
        Task<List<Appointment>> GetAllAppointments(CancellationToken cancellationToken);

        Task<(List<Appointment> Data, int TotalCount)> GetAppointmentsPaged(int pageNumber, int pageSize, CancellationToken cancellationToken);

        // Lấy cuộc hẹn theo ID
        Task<Appointment> GetAppointmentById(int appointmentId, CancellationToken cancellationToken);

        // Lấy danh sách cuộc hẹn của một trẻ em
        Task<List<Appointment>> GetAppointmentsByChildId(int childId, CancellationToken cancellationToken);

        // Lấy danh sách cuộc hẹn theo ngày
        Task<List<Appointment>> GetAppointmentsByDate(DateTime appointmentDate, CancellationToken cancellationToken);

        // Lấy danh sách cuộc hẹn theo trạng thái
        Task<List<Appointment>> GetAppointmentsByStatus(EnumList.AppointmentStatus status, CancellationToken cancellationToken);

        // Thêm mới cuộc hẹn
        Task<int> AddAppointment(Appointment appointment, CancellationToken cancellationToken);

        // Cập nhật thông tin cuộc hẹn
        Task<int> UpdateAppointment(Appointment appointment, CancellationToken cancellationToken);

        // Xóa cuộc hẹn theo ID
        Task<bool> DeleteAppointment(int appointmentId, CancellationToken cancellationToken);

        Task<int>GetQuantityAppointmentByPackageIdAndVaccineId(int childId, int packageId, int vaccineId, CancellationToken cancellationToken);
    }
}