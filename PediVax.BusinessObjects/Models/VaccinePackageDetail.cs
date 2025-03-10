using PediVax.BusinessObjects.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.Models
{
    [Table("VaccinePackageDetail")]
    public class VaccinePackageDetail
    {
        [Key]
        public int PackageDetailId { get; set; }
        public int PackageId { get; set; }
        public int VaccineId { get; set; }
        public int Quantity { get; set; }
        public EnumList.IsActive IsActive { get; set; }

        // Navigation properties
        [ForeignKey("PackageId")]
        public virtual VaccinePackage VaccinePackage { get; set; }

        [ForeignKey("VaccineId")]
        public virtual Vaccine Vaccine { get; set; }
    }
}
