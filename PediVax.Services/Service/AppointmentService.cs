using AutoMapper;
using PediVax.BusinessObjects.DTO.ReponseDTO;
using PediVax.BusinessObjects.DTO.RequestDTO;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PediVax.BusinessObjects.Enum;
using PediVax.BusinessObjects.DTO.ResponseDTO;

namespace PediVax.Services.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<List<AppointmentResponseDTO>> GetAllAppointments()
        {
            var appointments = await _appointmentRepository.GetAllAppointments();
            return _mapper.Map<List<AppointmentResponseDTO>>(appointments);
        }

        public async Task<AppointmentResponseDTO> GetAppointmentById(int appointmentId)
        {
            var appointment = await _appointmentRepository.GetAppointmentById(appointmentId);
            return _mapper.Map<AppointmentResponseDTO>(appointment);
        }

        public async Task<(List<AppointmentResponseDTO> Data, int TotalCount)> GetAppointmentsPaged(int pageNumber, int pageSize)
        {
            var (data, totalCount) = await _appointmentRepository.GetAppointmentsPaged(pageNumber, pageSize);
            var mappedData = _mapper.Map<List<AppointmentResponseDTO>>(data);
            return (mappedData, totalCount);
        }

        public async Task<AppointmentResponseDTO> CreateAppointment(CreateAppointmentDTO createAppointmentDTO)
        {
            var appointment = _mapper.Map<Appointment>(createAppointmentDTO);
            appointment.CreatedDate = DateTime.UtcNow;
            appointment.ModifiedDate = DateTime.UtcNow;
            appointment.AppointmentStatus = EnumList.AppointmentStatus.Pending; 

            var createdAppointment = await _appointmentRepository.AddAppointment(appointment);
            return _mapper.Map<AppointmentResponseDTO>(createdAppointment);
        }

        public async Task<bool> UpdateAppointment(int id, UpdateAppointmentDTO updateAppointmentDTO)
        {
            var existingAppointment = await _appointmentRepository.GetAppointmentById(id);
            if (existingAppointment == null)
            {
                throw new Exception("Appointment not found.");
            }

            _mapper.Map(updateAppointmentDTO, existingAppointment);
            existingAppointment.ModifiedDate = DateTime.UtcNow;

            var updated = await _appointmentRepository.UpdateAppointment(existingAppointment);
            return updated != null;
        }

        public async Task<bool> DeleteAppointment(int appointmentId)
        {
            return await _appointmentRepository.DeleteAppointment(appointmentId);
        }

        public async Task<List<AppointmentResponseDTO>> GetAppointmentsByChildId(int childId)
        {
            var appointments = await _appointmentRepository.GetAppointmentsByChildId(childId);
            return _mapper.Map<List<AppointmentResponseDTO>>(appointments);
        }

        public async Task<List<AppointmentResponseDTO>> GetAppointmentsByDate(DateTime appointmentDate)
        {
            var appointments = await _appointmentRepository.GetAppointmentsByDate(appointmentDate);
            return _mapper.Map<List<AppointmentResponseDTO>>(appointments);
        }

        public async Task<List<AppointmentResponseDTO>> GetAppointmentsByStatus(EnumList.AppointmentStatus status)
        {
            var appointments = await _appointmentRepository.GetAppointmentsByStatus(status);
            return _mapper.Map<List<AppointmentResponseDTO>>(appointments);
        }
    }
}
