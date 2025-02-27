using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.Models
{
    [Table("ChildProfile")]
    public class ChildProfile
    {
        [Key]
        public int ChildId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Relationship { get; set; }
        public string IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        // Navigation property
        public virtual User User { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<VaccineProfile> VaccineProfiles { get; set;}
    }
}
