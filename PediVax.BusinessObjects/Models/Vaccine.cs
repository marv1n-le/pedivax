﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.Models
{
    public class Vaccine
    {
        public int VaccineId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Origin { get; set; }
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
