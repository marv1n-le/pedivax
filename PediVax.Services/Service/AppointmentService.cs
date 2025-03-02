using AutoMapper;
using PediVax.BusinessObjects.DTO.ReponseDTO;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PediVax.BusinessObjects.Enum;
using PediVax.BusinessObjects.DTO.AppointmentDTO;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace PediVax.Services.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetCurrentUserName()
        {
            if (_httpContextAccessor.HttpContext == null || _httpContextAccessor.HttpContext.User == null)
            {
                return "System";
            }

            var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            return string.IsNullOrEmpty(userName) ? "System" : userName;
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
            appointment.CreatedBy = GetCurrentUserName();
            appointment.ModifiedDate = DateTime.UtcNow;
            appointment.AppointmentStatus = EnumList.AppointmentStatus.Pending;
            appointment.ModifiedBy = GetCurrentUserName();

            var createdAppointment = await _appointmentRepository.AddAppointment(appointment);
            return _mapper.Map<AppointmentResponseDTO>(appointment);
        }

        public async Task<bool> UpdateAppointment(int id, UpdateAppointmentDTO updateAppointmentDTO)
        {
            var existingAppointment = await _appointmentRepository.GetAppointmentById(id);
            if (existingAppointment == null)
            {
                throw new Exception("Appointment not found.");
            }
            else
            {

                _mapper.Map(updateAppointmentDTO, existingAppointment);
                existingAppointment.ModifiedDate = DateTime.UtcNow;
                existingAppointment.ModifiedBy = GetCurrentUserName();

                var updated = await _appointmentRepository.UpdateAppointment(existingAppointment);
                return true;
            }
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
