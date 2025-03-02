using System;
using System.ComponentModel.DataAnnotations;
using PediVax.BusinessObjects.Enum;

namespace PediVax.BusinessObjects.DTO.AppointmentDTO
{
    public class UpdateAppointmentDTO
    {
        [Required(ErrorMessage = "Payment ID is required")]
        public int PaymentId { get; set; }

        [Required(ErrorMessage = "Child ID is required")]
        public int ChildId { get; set; }

        public int VaccineId { get; set; } // Có thể không bắt buộc nếu là gói vaccine

        public int VaccinePackageId { get; set; } // Có thể không bắt buộc nếu là vaccine đơn lẻ

        [Required(ErrorMessage = "Appointment date is required")]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Appointment status is required")]
        public EnumList.AppointmentStatus AppointmentStatus { get; set; }

        [Required(ErrorMessage = "Active status is required")]
        public EnumList.IsActive IsActive { get; set; }

        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "ModifiedBy is required")]
        public string ModifiedBy { get; set; }
    }
}
