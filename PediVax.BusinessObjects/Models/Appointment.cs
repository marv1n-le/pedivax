using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.Models
{
    [Table("Appointment")]
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public int? PaymentDetailId { get; set; }
        public int UserId { get; set; }
        public int ChildId { get; set; }
        public int? VaccineId { get; set; }
        public int? VaccinePackageId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? Reaction { get; set; }
        public Enum.EnumList.AppointmentStatus AppointmentStatus { get; set; }
        public Enum.EnumList.IsActive IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        // Navigation properties
        [ForeignKey("PaymentDetailId")]
        public virtual PaymentDetail PaymentDetail { get; set; }

        [ForeignKey("ChildId")]
        public virtual ChildProfile ChildProfile { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("VaccineId")]
        public virtual Vaccine Vaccine { get; set; }

        [ForeignKey("VaccinePackageId")]
        public virtual VaccinePackage VaccinePackage { get; set; }
        public virtual ICollection<VaccineProfile> VaccineProfiles { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
