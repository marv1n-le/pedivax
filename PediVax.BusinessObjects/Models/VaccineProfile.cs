using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.Models
{
    public class VaccineProfile
    {
        [Key]
        public int VaccineProfileId { get; set; }
        public int AppointmentId { get; set; }
        public int ChildId { get; set; }
        public DateTime VaccinationDate { get; set; }
        public string Reaction { get; set; }
        public string IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        public Appointment Appointment { get; set; }
        public ChildProfile ChildProfile { get; set; }
        public ICollection<VaccineProfileDetail> VaccineProfileDetails { get; set; }
    }
}
