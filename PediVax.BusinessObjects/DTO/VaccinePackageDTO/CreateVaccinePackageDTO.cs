using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PediVax.BusinessObjects.DTO.VaccinePackageDTO
{
    public class CreateVaccinePackageDTO
    {
        [Required(ErrorMessage = "Package name is required")]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Total dose is required")]
        public int TotalDoses { get; set; }

        [Required(ErrorMessage = "Age in month is required")]
        public int AgeInMonths { get; set; }
    }
}
