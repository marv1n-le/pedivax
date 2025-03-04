using PediVax.BusinessObjects.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.AppointmentDTO
{
    public class CreateAppointmentDTO
    {
        [Required(ErrorMessage = "PaymentId is required")]
        public int PaymentId { get; set; }

       // [Required(ErrorMessage = "ChildId is required")]
        public int ChildId { get; set; }

        public int? VaccineId { get; set; }
        public int? VaccinePackageId { get; set; }

        [Required(ErrorMessage = "Appointment date is required")]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Appointment status is required")]
        public EnumList.AppointmentStatus AppointmentStatus { get; set; } = EnumList.AppointmentStatus.Pending;

        [Required(ErrorMessage = "IsActive status is required")]
        public EnumList.IsActive IsActive { get; set; } = EnumList.IsActive.Active;
    }
}
