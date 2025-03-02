using System.Text.Json.Serialization;
using PediVax.BusinessObjects.Enum;

namespace PediVax.BusinessObjects.DTO.ChildProfileDTO;

public class ChildProfileResponseDTO
{
    public int ChildId { get; set; }
    public int UserId { get; set; }
    public string FullName { get; set; }
    public string ImageUrl { get; set; }
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateTime DateOfBirth { get; set; }
    public EnumList.Gender Gender { get; set; }
    public EnumList.Relationship Relationship { get; set; }
}