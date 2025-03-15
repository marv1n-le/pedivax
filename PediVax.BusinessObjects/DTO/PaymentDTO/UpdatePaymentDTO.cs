using PediVax.BusinessObjects.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.PaymentDTO
{
    public class UpdatePaymentDTO
    {
        [Required(ErrorMessage = "Payment Id is required")]
        public int PaymentId { get; set; }


        public DateTime? PaymentDate { get; set; }
        public EnumList.PaymentStatus? PaymentStatus { get; set; }
    }
}
