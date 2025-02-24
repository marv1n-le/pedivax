using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.Models
{
    public class VaccineDose
    {
        public int DoseId { get; set; }
        public int VaccineId { get; set; }
        public string AgeRange { get; set; }
        public int DoseNumber { get; set; }
        public string Description { get; set; }
        public string IsActive { get; set; }

        // Navigation property
        public virtual Vaccine Vaccine { get; set; }
    }

}
