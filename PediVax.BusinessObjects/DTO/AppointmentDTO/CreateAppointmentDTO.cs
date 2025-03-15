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
        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "ChildId is required")]
        public int ChildId { get; set; }
        public int? PaymentDetailId { get; set; }
        public int? VaccineId { get; set; }
        public int? VaccinePackageId { get; set; }

        [Required(ErrorMessage = "Appointment date is required")]
        public DateTime AppointmentDate { get; set; }

    }
}
