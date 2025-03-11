
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using PediVax.BusinessObjects.Enum;

namespace PediVax.BusinessObjects.DTO.ChildProfileDTO;

public class UpdateChildProfileDTO
{
    [Required(ErrorMessage = "Child's name is required")]
    [MaxLength(50, ErrorMessage = "Child name cannot be longer than 50 characters")]
    public string FullName { get; set; }
    
    public IFormFile? ProfilePicture { get; set; }
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateTime? DateOfBirth { get; set; }
    public EnumList.Gender? Gender { get; set; }
    public EnumList.Relationship? Relationship { get; set; }
}