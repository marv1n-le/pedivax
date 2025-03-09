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
    [Table("VaccineSchedulePersonal")]
    public class VaccineSchedulePersonal
    {
        [Key]
        public int VaccineSchedulePersonalId { get; set; }
        public int VaccineScheduleId { get; set; }
        public int ChildId { get; set; }
        public int DiseaseId { get; set; }
        public int VaccineId { get; set; }
        public int DoseNumber { get; set; }
        public DateTime ScheduledDate { get; set; }
        public EnumList.IsCompleted IsCompleted { get; set; }
        public EnumList.IsActive IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        // Navigation properties

        [ForeignKey("ChildId")]
        public virtual ChildProfile ChildProfile { get; set; }

        [ForeignKey("DiseaseId")]
        public virtual Disease Disease { get; set; }

        [ForeignKey("VaccineScheduleId")]
        public virtual VaccineSchedule VaccineSchedule { get; set; }

        [ForeignKey("VaccineId")]
        public virtual Vaccine Vaccine { get; set; }
    }
}
