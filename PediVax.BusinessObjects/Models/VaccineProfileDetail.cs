using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.Models
{
    [Table("VaccineProfileDetail")]
    public class VaccineProfileDetail
    {
        [Key]
        public int Id { get; set; }
        public int VaccineProfileId { get; set; }
        public int VaccineId { get; set; }
        public int VaccineScheduleId { get; set; }
        public int DoseNumber { get; set; }
        public DateTime ScheduledDate { get; set; }
        public int IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }

        // Navigation properties
        public virtual VaccineSchedule VaccineSchedule { get; set; }
        public virtual VaccineProfile VaccineProfile { get; set; }
        public virtual Vaccine Vaccine { get; set; }
    }
}
