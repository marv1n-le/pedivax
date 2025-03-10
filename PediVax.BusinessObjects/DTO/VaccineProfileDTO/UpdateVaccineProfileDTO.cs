using PediVax.BusinessObjects.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.VaccineProfileDTO
{
    public class UpdateVaccineProfileDTO
    {
        [Required(ErrorMessage = "ChildId is required")]
        public int ChildId { get; set; }
        public int? AppointmentId { get; set; }
        public DateTime? VaccinationDate { get; set; }
        public int DiseaseId { get; set; }
        public EnumList.IsCompleted? IsCompleted { get; set; }
    }
}
