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



        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Total price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Total price must be a positive value")]
        public decimal TotalPrice { get; set; }

    }
}
