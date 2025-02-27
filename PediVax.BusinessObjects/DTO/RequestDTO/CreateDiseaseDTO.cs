using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.RequestDTO
{
    public class CreateDiseaseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IsActive { get; set; }
    }
}
