using PediVax.BusinessObjects.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.PaymentDTO
{
    public class PaymentResponseDTO
    {

        public int PaymentId { get; set; }

        public int UserId { get; set; }
        public int? VaccinePackageId { get; set; }
        public int? VaccineId { get; set; }
        public string PaymentType { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public EnumList.PaymentStatus PaymentStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public string URL { get; set; }
    }
}
