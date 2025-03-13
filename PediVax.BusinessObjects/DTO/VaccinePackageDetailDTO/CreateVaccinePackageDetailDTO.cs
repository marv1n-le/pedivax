using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.VaccinePackageDetailDTO
{
    public class CreateVaccinePackageDetailDTO
    {
        [Required(ErrorMessage = "VaccinePackageId is required")]
        public int VaccinePackageId { get; set; }
        [Required(ErrorMessage = "VaccineId is required")]
        public int VaccineId { get; set; }
        [Required(ErrorMessage = "DoseNumber is required")]
        public int DoseNumber { get; set; }
    }
}
