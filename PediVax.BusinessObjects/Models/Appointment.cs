using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int PaymentId { get; set; }
        public int ChildId { get; set; }
        public int VaccineId { get; set; }
        public int PackageId { get; set; }
        public int VaccineProfileId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        // Navigation properties
        public virtual Payment Payment { get; set; }
        public virtual VaccineProfile VaccineProfile { get; set; }
        public virtual ChildProfile ChildProfile { get; set; }
        public virtual Vaccine Vaccine { get; set; }
        public virtual VaccinePackage VaccinePackage { get; set; }
    }
}
