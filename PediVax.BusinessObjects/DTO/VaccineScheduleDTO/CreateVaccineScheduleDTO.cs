using PediVax.BusinessObjects.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.VaccineScheduleDTO
{
    public class CreateVaccineScheduleDTO
    {
        [Required(ErrorMessage = "DiseaseId is required")]
        public int DiseaseId { get; set; }
        [Required(ErrorMessage = "AgeInMonths is required")]
        public int AgeInMonths { get; set; }
        [Required(ErrorMessage = "DoseNumber is required")]
        public int DoseNumber { get; set; }
    }
}
