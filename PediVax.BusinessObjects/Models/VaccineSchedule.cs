using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PediVax.BusinessObjects.Enum;

namespace PediVax.BusinessObjects.Models
{
    [Table("VaccineSchedule")]
    public class VaccineSchedule
    {
        [Key]
        public int VaccineScheduleId { get; set; }
        public int DiseaseId { get; set; } 
        public int VaccineId { get; set; }
        public int AgeInMonths { get; set; }
        public int DoseNumber { get; set; }
        public EnumList.IsActive IsActive{ get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        // Navigation properties
        public virtual Disease Disease { get; set; }
        public virtual Vaccine Vaccine { get; set; }
    }
}