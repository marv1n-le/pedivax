using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.Models
{
    [Table("VaccinePackage")]
    public class VaccinePackage
    {
        [Key]
        public int PackageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal TotalPrice { get; set; }
        public Enum.EnumList.IsActive IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public ICollection<VaccinePackageDetail> VaccinePackageDetails { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<PaymentDetail> PaymentDetails { get; set; }
    }
}
