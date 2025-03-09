using PediVax.BusinessObjects.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.VaccineProfileDTO
{
    public class CreateVaccineProfileDTO
    {
        [Required(ErrorMessage = "ChildId is required")]
        public int ChildId { get; set; }
        [Required(ErrorMessage = "DiseaseId is required")]
        public int DiseaseId { get; set; }
        public DateTime? VaccinationDate { get; set; }
        [Required(ErrorMessage = "IsCompleted is required")]
        public EnumList.IsCompleted IsCompleted { get; set; }
    }
}
