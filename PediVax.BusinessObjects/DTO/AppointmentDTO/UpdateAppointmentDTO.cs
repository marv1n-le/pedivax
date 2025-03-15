using System;
using System.ComponentModel.DataAnnotations;
using PediVax.BusinessObjects.Enum;

namespace PediVax.BusinessObjects.DTO.AppointmentDTO
{
    public class UpdateAppointmentDTO
    {
        public int? UserId { get; set; }
        public int? PaymentDetailId { get; set; }
        public int? ChildId { get; set; }
        public int? VaccineId { get; set; }
        public int? VaccinePackageId { get; set; }
        public string? Reaction { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public EnumList.AppointmentStatus? AppointmentStatus { get; set; }
    }
}
