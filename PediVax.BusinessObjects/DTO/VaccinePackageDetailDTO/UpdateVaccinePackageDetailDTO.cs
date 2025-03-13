using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.VaccinePackageDetailDTO
{
    public class UpdateVaccinePackageDetailDTO
    {
        public int? VaccinePackageId { get; set; }
        public int? VaccineId { get; set; }
        public int? DoseNumber { get; set; }
    }
}
