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
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        public int PackageId { get; set; }
        public int VaccineId { get; set; }
    }
}
