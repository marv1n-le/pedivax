using PediVax.BusinessObjects.DTO.DiseaseDTO;
using PediVax.BusinessObjects.DTO.VaccineDTO;
using PediVax.BusinessObjects.Enum;
using PediVax.BusinessObjects.Models;
using System.Text.Json.Serialization;

namespace PediVax.BusinessObjects.DTO.VaccineScheduleDTO;

public class VaccineScheduleResponseDTO
{
    public int VaccineScheduleId { get; set; }
    public int DiseaseId { get; set; }
    public int AgeInMonths { get; set; }
    public int DoseNumber { get; set; }
    public EnumList.IsActive IsActive { get; set; }
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateTime ModifiedDate { get; set; }
    public string ModifiedBy { get; set; }

    public virtual DiseaseResponseDTO Disease { get; set; }
}