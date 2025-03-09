using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.Models
{
    [Table("Disease")]
    public class Disease
    {
        [Key]
        public int DiseaseId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Enum.EnumList.IsActive IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        // Navigation properties
        public virtual ICollection<VaccineSchedule> VaccineSchedules { get; set; }
        public virtual ICollection<VaccineDisease> VaccineDiseases { get; set; }
        public virtual ICollection<VaccineProfile> VaccineProfiles { get; set; }
        public virtual ICollection<VaccineSchedulePersonal> VaccineSchedulePersonals { get; set; }
    }
}
