using AutoMapper;
using PediVax.BusinessObjects.DTO.ReponseDTO;
using PediVax.BusinessObjects.DTO.RequestDTO;
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
            CreateMap<User, CreateUserDTO>();
            CreateMap<User, UserReponseDTO>();
        }
    }
}
