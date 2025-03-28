﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.Models
{
    [Table("VaccineDisease")]
    public class VaccineDisease
    {
        [Key]
        public int VaccineDiseaseId { get; set; }
        public int VaccineId { get; set; }
        public int DiseaseId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        // Navigation properties
        [ForeignKey("VaccineId")]
        public virtual Vaccine Vaccine { get; set; }
        [ForeignKey("DiseaseId")]
        public virtual Disease Disease { get; set; }
    }
}
