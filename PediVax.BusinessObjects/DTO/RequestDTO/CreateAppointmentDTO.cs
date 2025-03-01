using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.RequestDTO
{
    public class CreateAppointmentDTO
    {
        [Required]
        public int PaymentId { get; set; }

        [Required]
        public int ChildId { get; set; }

        [Required]
        public int VaccineId { get; set; }

        [Required]
        public int VaccinePackageId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
