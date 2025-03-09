using PediVax.BusinessObjects.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.VaccineSchedulePersonalDTO
{
    public class VaccineSchedulePersonalResponseDTO
    {
        public int VaccineSchedulePersonalId { get; set; }
        public int VaccineScheduleId { get; set; }
        public int ChildId { get; set; }
        public int DiseaseId { get; set; }
        public int VaccineId { get; set; }
        public int DoseNumber { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateTime ScheduledDate { get; set; }
        public EnumList.IsCompleted IsCompleted { get; set; }
        public EnumList.IsActive IsActive { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
