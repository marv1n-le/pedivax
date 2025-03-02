using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.Models
{
    [Table("Vaccine")]
    public class Vaccine
    {
        [Key]
        public int VaccineId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Origin { get; set; }
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Enum.EnumList.IsActive IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public ICollection<VaccineDose> VaccineDoses { get; set; }
        public ICollection<VaccineDisease> VaccineDiseases { get; set; }
        public ICollection<VaccinePackageDetail> VaccinePackageDetails { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<VaccineProfileDetail> VaccineProfileDetails { get; set; }
    }
}
