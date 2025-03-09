using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.Models
{
    [Table("PaymentDetail")]
    public class PaymentDetail
    {
        [Key]
        public int PaymentDetailId { get; set; }
        public int PaymentId { get; set; }
        public int VaccinePackageId { get; set; }
        public string IsCompleted { get; set; }
        public DateTime AdministeredDate { get; set; }
        public string Notes { get; set; }

        // Navigation properties
        [ForeignKey("PaymentId")]
        public Payment Payment { get; set; }

        [ForeignKey("VaccinePackageId")]
        public VaccinePackage VaccinePackage { get; set; }
    }
}
