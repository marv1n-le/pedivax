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
        
        public EnumList.PaymentStatus? PaymentStatus { get; set; }
    }
}
