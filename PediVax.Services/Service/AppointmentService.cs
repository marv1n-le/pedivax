using AutoMapper;
using PediVax.BusinessObjects.DTO.RequestDTO;
using PediVax.BusinessObjects.DTO.ResponseDTO;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Repositories.Repository;
using PediVax.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.Services.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public AppointmentService(IAppointmentRepository appointmenteRepository, IMapper mapper)
        {
            _appointmentRepository = appointmenteRepository;
            _mapper = mapper;
        }
        public async Task<AppointmentResponseDTO> AddAppointment(CreateAppointmentDTO createAppointmentDTO)
        {
            var appointment = _mapper.Map<Appointment>(createAppointmentDTO);
            var createdAppointment = await _appointmentRepository.AddAppointment(appointment);
            return _mapper.Map<AppointmentResponseDTO>(createdAppointment);
        }
        // xoa cuoc hen
        public async Task<bool> DeleteAppointment(int appointmentId)
        {
            return await _appointmentRepository.DeleteAppointment(appointmentId);
        }
        // get all
        public async Task<List<AppointmentResponseDTO>> GetAllAppointments()
        {
            var appointments = await _appointmentRepository.GetAllAppointments();
            return _mapper.Map<List<AppointmentResponseDTO>>(appointments);
        }
        // get by id cuoc hen
        public async Task<AppointmentResponseDTO> GetAppointmentById(int appointmentId)
        {
            var appointment = await _appointmentRepository.GetAppointmentById(appointmentId);
            return _mapper.Map<AppointmentResponseDTO>(appointment);
        }
        // get by id tre em
        public async Task<List<AppointmentResponseDTO>> GetAppointmentsByChildId(int childId)
        {
            var appointments = await _appointmentRepository.GetAppointmentsByChildId(childId);
            return _mapper.Map<List<AppointmentResponseDTO>>(appointments);
        }
        // get by time
        public async Task<List<AppointmentResponseDTO>> GetAppointmentsByDate(DateTime appointmentDate)
        {
            var appointments = await _appointmentRepository.GetAppointmentsByDate(appointmentDate);
            return _mapper.Map<List<AppointmentResponseDTO>>(appointments);
        }
        // get by status
        public async Task<List<AppointmentResponseDTO>> GetAppointmentsByStatus(string status)
        {
            var appointments = await _appointmentRepository.GetAppointmentsByStatus(status);
            return _mapper.Map<List<AppointmentResponseDTO>>(appointments);
        }
        // update
        public async Task<AppointmentResponseDTO> UpdateAppointment(CreateAppointmentDTO createAppointmentDTO)
        {
            var appointment = _mapper.Map<Appointment>(createAppointmentDTO);
            var updatedAppointment = await _appointmentRepository.UpdateAppointment(appointment);
            return _mapper.Map<AppointmentResponseDTO>(updatedAppointment);
        }
    }
}
