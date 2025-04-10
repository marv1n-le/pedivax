﻿using PediVax.BusinessObjects.DTO.VaccineScheduleDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.Services.IService
{
    public interface IVaccineScheduleService
    {
        Task<List<VaccineScheduleResponseDTO>> GetAllVaccineSchedule(CancellationToken cancellationToken);
        Task<VaccineScheduleResponseDTO> GetVaccineScheduleById(int id, CancellationToken cancellationToken);
        Task<VaccineScheduleResponseDTO> CreateVaccineSchedule(CreateVaccineScheduleDTO vaccineSchedule, CancellationToken cancellationToken);
        Task<bool> UpdateVaccineSchedule(int vaccineScheduleId, UpdateVaccineScheduleDTO updateVaccineScheduleDTO, CancellationToken cancellationToken);
        Task<bool> DeleteVaccineSchedule(int vaccineScheduleId, CancellationToken cancellationToken);
    }
}
