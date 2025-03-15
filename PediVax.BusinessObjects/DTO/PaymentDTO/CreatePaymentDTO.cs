using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.PaymentDTO
{
    public class CreatePaymentDTO
    {
        
        public int? VaccinePackageId { get; set; }

        
        public int? VaccineId { get; set; }

        [Required(ErrorMessage = "Payment Type is required")]
        public string PaymentType { get; set; }

        //[Required(ErrorMessage = "Total Amount is required")]
        //[Range(0, double.MaxValue, ErrorMessage = "Total Amount must be a positive value")]
        //public decimal TotalAmount { get; set; }


    }
}
