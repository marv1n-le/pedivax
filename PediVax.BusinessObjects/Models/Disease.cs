﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.Models
{
    public class Disease
    {
        [Key]
        public int DiseaseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public ICollection<VaccineDisease> VaccineDiseases { get; set; }
    }
}
