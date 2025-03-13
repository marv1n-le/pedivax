using PediVax.BusinessObjects.DTO.VaccineDTO;
using PediVax.BusinessObjects.DTO.VaccinePackageDTO;
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
        public int VaccinePackageDetailId { get; set; }
        public int VaccinePackageId { get; set; }
        public int VaccineId { get; set; }
        public int DoseNumber { get; set; }
        public string IsActive { get; set; }

        public VaccinePackageResponseDTO VaccinePackage { get; set; }
        public VaccineResponseDTO Vaccine { get; set; }
    }
}
