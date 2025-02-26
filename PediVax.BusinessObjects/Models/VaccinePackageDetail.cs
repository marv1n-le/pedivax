using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.Models
{
    public class VaccinePackageDetail
    {
        [Key]
        public int PackageDetailId { get; set; }
        public int PackageId { get; set; }
        public int VaccineId { get; set; }
        public int Quantity { get; set; }
        public string IsActive { get; set; }

        public VaccinePackage VaccinePackage { get; set; }
        public Vaccine Vaccine { get; set; }
    }
}
