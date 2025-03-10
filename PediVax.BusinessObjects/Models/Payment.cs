﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.Models
{
    [Table("Payment")]
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public int? VaccinePackageId { get; set; }
        public int? VaccineId { get; set; }
        public string? PaymentType { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        // Navigation properties
        [ForeignKey("VaccinePackageId")]
        public virtual VaccinePackage VaccinePackage { get; set; }
        [ForeignKey("VaccineId")]
        public virtual Vaccine Vaccine { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<PaymentDetail> PaymentDetails { get; set; }
    }
}
