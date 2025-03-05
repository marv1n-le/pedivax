using PediVax.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.VaccinePackageDetailDTO
{
    public class VaccinePackageDetailResponseDTO
    {
        public int PackageDetailId { get; set; }
        public int PackageId { get; set; }
        public int VaccineId { get; set; }
        public int Quantity { get; set; }
        public string IsActive { get; set; }

        public VaccinePackage VaccinePackage { get; set; }
        public Vaccine Vaccine { get; set; }
    }
}
