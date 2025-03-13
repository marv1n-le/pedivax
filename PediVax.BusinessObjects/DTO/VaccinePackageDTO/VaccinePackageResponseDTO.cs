using PediVax.BusinessObjects.DTO.VaccinePackageDetailDTO;
using PediVax.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.VaccinePackageDTO
{
    public class VaccinePackageResponseDTO
    {
        public int VaccinePackageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalDoses { get; set; }
        public int AgeInMonths { get; set; }
        public Enum.EnumList.IsActive IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public List<VaccinePackageDetailResponseDTO> VaccinePackageDetails { get; set; }
    }
}
