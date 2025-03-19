using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PediVax.BusinessObjects.DTO.VaccineDTO;

public class CreateVaccineDTO
{
    [Required(ErrorMessage = ("Name is required"))]
    public string Name { get; set; }
    [Required(ErrorMessage = ("Image is required"))]
    public IFormFile Image { get; set; }
    [Required(ErrorMessage = ("Description is required"))]
    public string Description { get; set; }
    [Required(ErrorMessage = ("Quantity is required"))]
    public int Quantity { get; set; }
    [Required(ErrorMessage = ("Origin is required"))]
    public string Origin { get; set; }
    [Required(ErrorMessage = ("Manufacturer is required"))]
    public string Manufacturer { get; set; }
    [Required(ErrorMessage = ("Price is required"))]
    public decimal Price { get; set; }
    [Required(ErrorMessage = ("Date of manufacture is required"))]
    public DateTime DateOfManufacture { get; set; }
    [Required(ErrorMessage = ("Expiry date is required"))]
    public DateTime ExpiryDate { get; set; }
}