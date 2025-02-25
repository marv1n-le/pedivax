using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.Models
{
    public class VaccineProfile
    {
        public int VaccineProfileId { get; set; }
        public int ChildId { get; set; }
        public int VaccineId { get; set; }
        public DateTime VaccinationDate { get; set; }
        public string Reaction { get; set; }
        public string IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        // Mối quan hệ với Appointment
        public virtual ICollection<Appointment> Appointments { get; set; }

        // Navigation properties
        public virtual ChildProfile ChildProfile { get; set; }
        public virtual Vaccine Vaccine { get; set; }
        public virtual ICollection<VaccineProfileDetail> VaccineProfileDetails { get; set; }
    }
}
