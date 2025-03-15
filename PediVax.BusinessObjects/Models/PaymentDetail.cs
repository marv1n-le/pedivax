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
    [Table("PaymentDetail")]
    public class PaymentDetail
    {
        [Key]
        public int PaymentDetailId { get; set; }
        public int PaymentId { get; set; }
        public int VaccinePackageDetailId { get; set; }
        public EnumList.IsCompleted IsCompleted { get; set; }
        public DateTime? AdministeredDate { get; set; }
        public string? Notes { get; set; }
        public int DoseSequence { get; set; }
        public DateTime? ScheduledDate { get; set; }

        // Navigation properties
        [ForeignKey("PaymentId")]
        public virtual Payment Payment { get; set; }

        [ForeignKey("VaccinePackageDetailId")]
        public virtual VaccinePackageDetail VaccinePackageDetail { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
