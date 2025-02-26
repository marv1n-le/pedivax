using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public int VaccinePackageId { get; set; }
        public int VaccineId { get; set; }
        public int TotalAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        // Navigation properties
        public virtual VaccinePackage VaccinePackage { get; set; }
        public virtual Vaccine Vaccine { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<PaymentDetail> PaymentDetails { get; set; }
    }
}
