using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PediVax.BusinessObjects.Enum;

namespace PediVax.BusinessObjects.DTO.PaymentDetailDTO
{
    public class UpdatePaymentDetailDTO
    {
        public EnumList.IsActive IsCompleted { get; set; }
        public DateTime? AdministeredDate { get; set; }
        public string Notes { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public int? AppointmentId { get; set; }
    }
}
