using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.VaccineProfileDTO
{
    public class UpdateVaccineProfileDTO
    {
        public int? ChildId { get; set; }
        public DateTime? VaccinationDate { get; set; }
        public string? Reaction { get; set; }
        public List<int>? VaccinatedDiseaseIds { get; set; } = new List<int>();
        public List<int>? VaccinatedVaccineScheduleIds { get; set; } = new List<int>();
    }
}
