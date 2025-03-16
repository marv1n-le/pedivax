using System.Text.Json.Serialization;
using PediVax.BusinessObjects.DTO.ReponseDTO;
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
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateTime ModifiedDate { get; set; }
    public string ModifiedBy { get; set; }
    public EnumList.IsActive IsActive { get; set; }

    public UserResponseDTO User { get; set; }
}