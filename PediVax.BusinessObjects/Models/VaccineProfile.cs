using PediVax.BusinessObjects.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.Models
{
    [Table("VaccineProfile")]
    public class VaccineProfile
    {
        [Key]
        public int VaccineProfileId { get; set; }
        public int? AppointmentId { get; set; }
        public int ChildId { get; set; }
        public int DiseaseId { get; set; }
        public DateTime? VaccinationDate { get; set; }
        public EnumList.IsCompleted IsCompleted { get; set; }
        public EnumList.IsActive IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        // Navigation properties
        public virtual Appointment Appointment { get; set; }

        [ForeignKey("ChildId")]
        public virtual ChildProfile ChildProfile { get; set; }

        [ForeignKey("DiseaseId")]
        public virtual Disease Disease { get; set; }

    }
}
