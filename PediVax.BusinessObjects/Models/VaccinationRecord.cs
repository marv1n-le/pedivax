﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.Models
{
    [Table("VaccinationRecord")]
    public class VaccinationRecord
    {
        [Key]
        public int RecordId { get; set; }
        public int AppointmentId { get; set; }
        public DateTime AdministeredDate { get; set; }
        public string Reaction { get; set; }
        public string? Notes { get; set; }
        public Enum.EnumList.IsActive IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        // Navigation property
        [ForeignKey("AppointmentId")]
        public virtual Appointment Appointment { get; set; }
    }
}
