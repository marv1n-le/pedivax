using PediVax.BusinessObjects.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.VaccineProfileDTO
{
    public class VaccineProfileResponseDTO
    {
        public int VaccineProfileId { get; set; }
        public int? AppointmentId { get; set; }
        public int ChildId { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateTime? VaccinationDate { get; set; }
        public EnumList.IsCompleted IsCompleted { get; set; }
        public EnumList.IsActive IsActive { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateTime CreatedDate { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
