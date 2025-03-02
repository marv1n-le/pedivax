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
        public int PaymentId { get; set; }
        public int ChildId { get; set; }
        public int VaccineId { get; set; }
        public int VaccinePackageId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public Enum.EnumList.AppointmentStatus AppointmentStatus { get; set; }
        public Enum.EnumList.IsActive IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public Payment Payment { get; set; }
        public ChildProfile ChildProfile { get; set; }
        public Vaccine Vaccine { get; set; }
        public VaccinePackage VaccinePackage { get; set; }
        public ICollection<VaccinationRecord> VaccinationRecords { get; set; }
        public ICollection<VaccineProfile> VaccineProfiles { get; set; }
    }
}
