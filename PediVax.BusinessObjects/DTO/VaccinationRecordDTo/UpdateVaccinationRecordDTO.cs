using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.VaccinationRecordDTo
{
    public class UpdateVaccinationRecordDTO
    {
       // [Required(ErrorMessage = "Reaction is required")]
        public string? Reaction { get; set; }

       // [Required(ErrorMessage = "Notes is required")]
        public string? Notes { get; set; } 

        public Enum.EnumList.IsActive IsActive { get; set; }
    }
}
