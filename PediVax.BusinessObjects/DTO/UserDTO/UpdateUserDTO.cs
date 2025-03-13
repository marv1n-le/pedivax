using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.RequestDTO
{
    public class UpdateUserDTO
    {
        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? FullName { get; set; }

        public IFormFile? Image { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public Enum.EnumList.Role? Role { get; set; }

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public string? DateOfBirth { get; set; }
    }
}
