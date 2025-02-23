using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int PackageId { get; set; }
        public int VaccineId { get; set; }
        public int TotalAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        // Navigation properties
        public virtual VaccinePackage VaccinePackage { get; set; }
        public virtual Vaccine Vaccine { get; set; }
    }
}
