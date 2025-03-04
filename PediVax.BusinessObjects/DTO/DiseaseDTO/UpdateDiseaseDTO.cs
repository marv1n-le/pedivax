using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.DiseaseDTO
{
     public class UpdateDiseaseDTO
    {
        public string? Name { get; set; } // có the nulll
        public string? Description { get; set; } // có the nulll
        public Enum.EnumList.IsActive IsActive { get; set; }
    }
}
