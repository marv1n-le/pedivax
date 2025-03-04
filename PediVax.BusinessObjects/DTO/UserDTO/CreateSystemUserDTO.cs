using PediVax.BusinessObjects.Enum;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PediVax.BusinessObjects.DTO.RequestDTO;

public class CreateSystemUserDTO
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Full name is required")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Invalid phone number format")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Image is required")]
    public IFormFile Image { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Date of birth is required")]
    public DateTime DateOfBirth { get; set; }
    
    [Required(ErrorMessage = "Role is required")]
    public EnumList.Role Role { get; set; }

}