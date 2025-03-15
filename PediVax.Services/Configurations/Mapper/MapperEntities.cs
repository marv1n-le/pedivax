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
using PediVax.BusinessObjects.DTO.VaccinePackageDTO;
using PediVax.BusinessObjects.DTO.VaccinePackageDetailDTO;
using PediVax.BusinessObjects.DTO.VaccineScheduleDTO;
using PediVax.BusinessObjects.DTO.VaccineProfileDTO;
using PediVax.BusinessObjects.DTO.VaccineSchedulePersonalDTO;
using System.Globalization;
using PediVax.BusinessObjects.Enum;
using PediVax.BusinessObjects.DTO.PaymentDTO;

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
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image == null ? null : src.Image))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src =>
    DateTime.ParseExact(src.DateOfBirth.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture))); ;

            //Vaccine Mapper
            CreateMap<CreateVaccineDTO, Vaccine>();
            CreateMap<UpdateVaccineDTO, Vaccine>();
            CreateMap<Vaccine, VaccineResponseDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image == null ? null : src.Image));

            //Disease Mapper
            CreateMap<CreateDiseaseDTO, Disease>();
            CreateMap<Disease, DiseaseResponseDTO>();

            //Appointment Mapper
            CreateMap<CreateAppointmentDTO, Appointment>();
            CreateMap<UpdateAppointmentDTO, Appointment>();
            CreateMap<Appointment, AppointmentResponseDTO>()
                .ForMember(dest => dest.VaccineName, opt => opt.MapFrom(src => src.Vaccine != null ? src.Vaccine.Name : null))
                .ForMember(dest => dest.VaccinePackageName, opt => opt.MapFrom(src => src.VaccinePackage != null ? src.VaccinePackage.Name : null));


            //VaccinePackage Mappper 
            CreateMap<CreateVaccinePackageDTO, VaccinePackage>();
            CreateMap<UpdateVaccinePackageDTO, VaccinePackage>();
            CreateMap<VaccinePackage, VaccinePackageResponseDTO>()
                 .ForMember(dest => dest.VaccinePackageDetails, opt => opt.MapFrom(src => src.VaccinePackageDetails)).ReverseMap();

            //VaccinePackageDetail Mapper
            CreateMap<CreateVaccinePackageDetailDTO, VaccinePackageDetail>();
            CreateMap<UpdateVaccinePackageDetailDTO, VaccinePackageDetail>();
            CreateMap<VaccinePackageDetail, VaccinePackageDetailResponseDTO>();

            //VaccineSchedule Mapper
            CreateMap<CreateVaccineScheduleDTO, VaccineSchedule>();
            CreateMap<UpdateVaccineScheduleDTO, VaccineSchedule>();
            CreateMap<VaccineSchedule, VaccineScheduleResponseDTO>();

            //VaccineProfile Mapper
            CreateMap<CreateVaccineProfileDTO, VaccineProfile>();
            CreateMap<UpdateVaccineProfileDTO, VaccineProfile>();
            CreateMap<VaccineProfile, VaccineProfileResponseDTO>();

            //Payment 
            CreateMap<CreatePaymentDTO, Payment>();
            CreateMap<UpdatePaymentDTO, Payment>();
            CreateMap<Payment, PaymentResponseDTO>()
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(dest => dest.VaccineId, opt => opt.MapFrom(src => src.VaccineId))
            .ForMember(dest => dest.VaccinePackageId, opt => opt.MapFrom(src => src.VaccinePackageId))
            .ReverseMap();

        }
    }
}
