using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.VaccinationRecordDTo
{
    public class VaccinationRecordRequestDTO
    {
        public int RecordId { get; set; }
        public int AppointmentId { get; set; }
        public DateTime AdministeredDate { get; set; }
        public string Reaction { get; set; }
        public string Notes { get; set; }
        public Enum.EnumList.IsActive IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
