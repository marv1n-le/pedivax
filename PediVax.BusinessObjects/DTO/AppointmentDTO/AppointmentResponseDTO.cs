﻿using PediVax.BusinessObjects.DTO.ChildProfileDTO;
using PediVax.BusinessObjects.DTO.ReponseDTO;
using PediVax.BusinessObjects.DTO.VaccineDTO;
using PediVax.BusinessObjects.DTO.VaccinePackageDTO;
using PediVax.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.DTO.AppointmentDTO
{
    public class AppointmentResponseDTO
    {
        public int AppointmentId { get; set; }
        public int PaymentDetailId { get; set; }
        public int UserId { get; set; }
        public int ChildId { get; set; }
        public int VaccineId { get; set; }
        public int VaccinePackageId { get; set; }
        public string Reaction { get; set; }
        public DateTime AppointmentDate { get; set; }
        public Enum.EnumList.AppointmentStatus AppointmentStatus { get; set; }
        public Enum.EnumList.IsActive IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public UserResponseDTO User { get; set; }
        public VaccineResponseDTO Vaccine { get; set; }
        public VaccinePackageResponseDTO VaccinePackage { get; set; }

    }
}
