using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using PediVax.BusinessObjects.Enum;

namespace PediVax.BusinessObjects.DTO.ChildProfileDTO;

public class CreateChildProfileDTO
{
    [Required(ErrorMessage = "UserId is required for child profile")]
    public int UserId { get; set; }
    
    [Required(ErrorMessage = "Child's name is required")]
    [MaxLength(50, ErrorMessage = "Child name cannot be longer than 50 characters")]
    public string FullName { get; set; }
    
    [Required(ErrorMessage = "Child's date of birth is required")]
    public DateTime DateOfBirth { get; set; }
    
    [Required(ErrorMessage = "Child's gender is required")]
    public EnumList.Gender Gender { get; set; }
    
    [Required(ErrorMessage = "Relationship is required")]
    public EnumList.Relationship Relationship { get; set; }
    
    public IFormFile? ProfilePicture { get; set; }
}