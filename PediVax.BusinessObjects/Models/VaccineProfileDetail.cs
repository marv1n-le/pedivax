using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.Models
{
    public class VaccineProfileDetail
    {
        [Key]
        public int Id { get; set; }
        public int VaccineProfileId { get; set; }
        public int VaccineId { get; set; }
        public int DoseNumber { get; set; }
        public DateTime CreatedDate { get; set; }

        // Navigation properties
        public virtual VaccineProfile VaccineProfile { get; set; }
        public virtual Vaccine Vaccine { get; set; }
    }
}
