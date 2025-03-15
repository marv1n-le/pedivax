using PediVax.BusinessObjects.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.PaymentDetailDTO
{
    public class PaymentDetailResponseDTO
    {
        public int PaymentDetailId { get; set; }
        public int PaymentId { get; set; }
        public int VaccinePackageDetailId { get; set; }
        public EnumList.IsActive IsCompleted { get; set; }
        public DateTime? AdministeredDate { get; set; }
        public string Notes { get; set; }
        public int DoseSequence { get; set; }
        public DateTime? ScheduledDate { get; set; }

        public decimal Price { get; set; }
    }
}
