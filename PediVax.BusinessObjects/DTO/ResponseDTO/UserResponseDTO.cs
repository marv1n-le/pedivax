using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.ReponseDTO
{
    public class UserResponseDTO
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateTime DateOfBirth { get; set; }
        public string IsActive { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
