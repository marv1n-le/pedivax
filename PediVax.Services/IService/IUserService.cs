using PediVax.BusinessObjects.DTO.ReponseDTO;
using PediVax.BusinessObjects.DTO.RequestDTO;
using PediVax.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.Services.IService
{
    public interface IUserService
    {
        Task<List<UserResponseDTO>> GetAllUser();
        Task<UserResponseDTO> GetUserById(int userId);
        Task<(List<UserResponseDTO> Data, int TotalCount)> GetUserPaged(int pageNumber, int pageSize);
        Task<UserResponseDTO> CreateUser(CreateUserDTO createUserDTO);
        Task<bool> UpdateUser(int id,UpdateUserDTO updateUserDTO);
        Task<bool> DeleteUser(int userId);
        Task<UserResponseDTO> GetUserByEmail(string email);
        Task<List<UserResponseDTO>> GetUserByName(string keyword);
    }
}
