using System.Text.Json.Serialization;

namespace PediVax.BusinessObjects.DTO.VaccineDTO;

public class VaccineResponseDTO
{
    public int VaccineId { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public string Origin { get; set; }
    public string Manufacturer { get; set; }
    public decimal Price { get; set; }
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateTime DateOfManufacture { get; set; }
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateTime ExpiryDate { get; set; }
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateTime ModifiedDate { get; set; }
    public string ModifiedBy { get; set; }
    public Enum.EnumList.IsActive IsActive { get; set; }
}