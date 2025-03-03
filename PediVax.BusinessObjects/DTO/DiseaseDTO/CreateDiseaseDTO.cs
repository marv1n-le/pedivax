using System;
using System.ComponentModel.DataAnnotations;
using PediVax.BusinessObjects.Enum;

namespace PediVax.BusinessObjects.DTO.DiseaseDTO;

public class CreateDiseaseDTO
{
    [Required(ErrorMessage = "Disease name is required")]
    [MaxLength(255, ErrorMessage = "Disease name cannot be longer than 255 characters")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Disease description is required")]
    [MaxLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters")]
    public string Description { get; set; }

    [Required(ErrorMessage = "IsActive status is required")]
    public EnumList.IsActive IsActive { get; set; }
}
