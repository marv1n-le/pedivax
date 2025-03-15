using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.VaccineDiseaseDTO
{
    public class VaccineDiseaseResponseDTO
    {
        public int VaccineDiseaseId { get; set; }
        public int VaccineId { get; set; }
        public int DiseaseId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
