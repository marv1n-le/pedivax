using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PediVax.BusinessObjects.DTO.RequestDTO;
using PediVax.BusinessObjects.DTO.ResponseDTO;

namespace PediVax.Services.IService
{
    public interface IAppointmentService
    {
        // Lấy hết danh sách cuộc hẹn 
        Task<List<AppointmentResponseDTO>> GetAllAppointments();

        // Lấy cuộc hẹn theo ID
        Task<AppointmentResponseDTO> GetAppointmentById(int appointmentId);

        // Lấy cuộc hẹn theo ID trẻ em
        Task<List<AppointmentResponseDTO>> GetAppointmentsByChildId(int childId);

        // Lấy các cuộc hẹn theo ngày
        Task<List<AppointmentResponseDTO>> GetAppointmentsByDate(DateTime appointmentDate);
        
        // Thêm cuộc hẹn
        Task<AppointmentResponseDTO> AddAppointment(CreateAppointmentDTO createAppointmentDTO);

        // Cập nhật cuộc hẹn
        Task<AppointmentResponseDTO> UpdateAppointment(CreateAppointmentDTO createAppointmentDTO);

        // Hủy, xóa cuộc hẹn
        Task<bool> DeleteAppointment(int appointmentId);
    }
}
