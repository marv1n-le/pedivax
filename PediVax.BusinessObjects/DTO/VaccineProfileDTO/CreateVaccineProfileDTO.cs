using PediVax.BusinessObjects.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.VaccineProfileDTO
{
    public class CreateVaccineProfileDTO
    {
        public int ChildId { get; set; }
        public int DiseaseId { get; set; }
        public DateTime? VaccinationDate { get; set; }
        public EnumList.IsCompleted IsCompleted { get; set; }
    }
}
