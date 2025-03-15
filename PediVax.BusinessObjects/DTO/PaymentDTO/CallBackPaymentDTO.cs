using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.PaymentDTO
{
    public class CallBackPaymentDTO
    {
        public bool Success { get; set; }
        public string PaymentId { get; set; }
        //public string TotalAmout { get; set; }
        //public string PayDate { get; set; }
    }
}
