using PediVax.BusinessObjects.Enum;

namespace PediVax.BusinessObjects.DTO.VaccineScheduleDTO;

public class UpdateVaccineScheduleDTO
{
    public int? DiseaseId { get; set; }
    public int? VaccineId { get; set; }
    public int? AgeInMonths { get; set; }
    public int? DoseNumber { get; set; }
}