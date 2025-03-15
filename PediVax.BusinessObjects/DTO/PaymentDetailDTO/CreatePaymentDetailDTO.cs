using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.PaymentDetailDTO
{
    public class CreatePaymentDetailDTO
    {
        [Required(ErrorMessage = ("PaymentId is required !"))]
        public int PaymentId { get; set; }
        [Required(ErrorMessage = ("VaccinePackageDetailId is required !"))]
        public int VaccinePackageDetailId { get; set; }
        public DateTime? ScheduledDate { get; set; }
    }
}
