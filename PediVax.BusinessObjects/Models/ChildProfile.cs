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
        public string Image { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Enum.EnumList.Gender Gender { get; set; }
        public Enum.EnumList.Relationship Relationship { get; set; }
        public Enum.EnumList.IsActive IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        // Navigation property
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<VaccineProfile> VaccineProfiles { get; set;}
        public virtual ICollection<VaccineSchedulePersonal> VaccineSchedulePersonals { get; set; }
    }
}
