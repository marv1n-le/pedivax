using AutoMapper;
using PediVax.BusinessObjects.DTO.ReponseDTO;
using PediVax.BusinessObjects.DTO.RequestDTO;
using PediVax.BusinessObjects.DTO.ResponseDTO;
using PediVax.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.Services.Configuration.Mapper
{
    public class MapperEntities : Profile
    {
        public MapperEntities()
        {
            CreateMap<CreateUserDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore()) 
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow)) 
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => "System"))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => "System"));
            CreateMap<User, UserResponseDTO>();
//Mapper Disease
            CreateMap<CreateDiseaseDTO, Disease>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => "System"))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => "System"))
               .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => ConvertIsActive(src.IsActive)));

            CreateMap<Disease, DiseaseResponseDTO>();

        }
        private static bool ConvertIsActive(string isActive)
        {
            return bool.TryParse(isActive, out bool result) && result;
        }

    }
}
