using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.VaccinePackageDTO
{
    public class UpdateVaccinePackageDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? TotalDoses { get; set; }
        public int? AgeInMonths { get; set; }
    }
}
