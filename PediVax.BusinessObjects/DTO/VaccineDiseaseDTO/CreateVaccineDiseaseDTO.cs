using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.VaccineDiseaseDTO
{
    public class CreateVaccineDiseaseDTO
    {
        [Required(ErrorMessage = "VaccineId is required")]
        public int VaccineId { get; set; }
        [Required(ErrorMessage = "DiseaseId is required")]
        public int DiseaseId { get; set; }
    }
}
