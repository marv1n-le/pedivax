using Microsoft.AspNetCore.Http;

namespace PediVax.BusinessObjects.DTO.VaccineDTO;

public class UpdateVaccineDTO
{
    public string? Name { get; set; }
    public IFormFile? Image { get; set; }
    public string? Description { get; set; }
    public string? Origin { get; set; }
    public string? Manufacturer { get; set; }
    public decimal? Price { get; set; }
    public DateTime? DateOfManufacture { get; set; }
    public DateTime? ExpiryDate { get; set; }
}