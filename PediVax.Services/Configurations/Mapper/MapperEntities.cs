using AutoMapper;
using PediVax.BusinessObjects.DTO.ReponseDTO;
using PediVax.BusinessObjects.DTO.RequestDTO;
using PediVax.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PediVax.BusinessObjects.DTO.ChildProfileDTO;
using PediVax.BusinessObjects.DTO.AppointmentDTO;
using PediVax.BusinessObjects.DTO.DiseaseDTO;
using PediVax.BusinessObjects.DTO.VaccineDTO;
using PediVax.BusinessObjects.DTO.VaccinationRecordDTo;
using PediVax.BusinessObjects.DTO.VaccinePackageDTO;
using PediVax.BusinessObjects.DTO.VaccinePackageDetailDTO;
using PediVax.BusinessObjects.DTO.VaccineScheduleDTO;

namespace PediVax.Services.Configuration.Mapper
{
    public class MapperEntities : Profile
    {
        public MapperEntities()
        {
            //User Mapper
            CreateMap<CreateUserDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());
            CreateMap<UpdateUserDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());
            CreateMap<CreateSystemUserDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());
            CreateMap<User, UserResponseDTO>();
            CreateMap<User, UserResponseDTO>()
                .ForMember(dest => dest.ChildProfile, opt => opt.MapFrom(src => src.ChildProfiles));


            //ChildProfile Mapper
            CreateMap<CreateChildProfileDTO, ChildProfile>();
            CreateMap<UpdateChildProfileDTO, ChildProfile>();
            CreateMap<ChildProfile, ChildProfileResponseDTO>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image == null ? null : src.Image));
            
            //Vaccine Mapper
            CreateMap<CreateVaccineDTO, Vaccine>();
            CreateMap<UpdateVaccineDTO, Vaccine>();
            CreateMap<Vaccine, VaccineResponseDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image == null ? null : src.Image));


            //Disease_mapper
            CreateMap<CreateDiseaseDTO, Disease>();
            CreateMap<Disease, DiseaseResponseDTO>();
            //Appointment_mapper

            CreateMap<CreateAppointmentDTO, Appointment>();
            CreateMap<UpdateAppointmentDTO, Appointment>();
            CreateMap<Appointment, AppointmentResponseDTO>();
            //VaccinationRecord_mapper
            CreateMap<CreateVaccinationRecordDTO, VaccinationRecord>();
            CreateMap<UpdateVaccinationRecordDTO, VaccinationRecord>();
            CreateMap<VaccinationRecord, VaccinationRecordRequestDTO>();

            //VaccinePackage_mappper 
            CreateMap<CreateVaccinePackageDTO, VaccinePackage>();
            CreateMap<UpdateVaccinePackageDTO, VaccinePackage>();
            CreateMap<VaccinePackage, VaccinePackageResponseDTO>();

            //VaccinePackageDetail_mapper
            CreateMap<CreateVaccinePackageDetailDTO, VaccinePackageDetail>();
            CreateMap<UpdateVaccinePackageDetailDTO, VaccinePackageDetail>();
            CreateMap<VaccinePackageDetail, VaccinePackageDetailResponseDTO>();

            //VaccineSchedule Mapper
            CreateMap<CreateVaccineScheduleDTO, VaccineSchedule>();
            CreateMap<UpdateVaccineScheduleDTO, VaccineSchedule>();
            CreateMap<VaccineSchedule, VaccineScheduleResponseDTO>();
        }
    }
}
