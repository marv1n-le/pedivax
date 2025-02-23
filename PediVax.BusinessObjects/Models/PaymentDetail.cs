using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.Models
{
    public class PaymentDetail
    {
        public int PaymentDetailId { get; set; }
        public int PaymentId { get; set; }
        public int PackageDetailId { get; set; }
        public string IsCompleted { get; set; }
        public DateTime AdministeredDate { get; set; }
        public string Notes { get; set; }

        // Navigation property
        public virtual Payment Payment { get; set; }
        public virtual VaccinePackageDetail VaccinePackageDetail { get; set; }
    }
}
