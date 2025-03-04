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
        Task<List<UserResponseDTO>> GetAllUser(CancellationToken cancellationToken);
        Task<UserResponseDTO> GetUserById(int userId, CancellationToken cancellationToken);
        Task<(List<UserResponseDTO> Data, int TotalCount)> GetUserPaged(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<UserResponseDTO> CreateUser(CreateUserDTO createUserDTO, CancellationToken cancellationToken);
        Task<UserResponseDTO> CreateSystemUser(CreateSystemUserDTO createSystemUserDTO, CancellationToken cancellationToken);
        Task<bool> UpdateUser(int id,UpdateUserDTO updateUserDTO, CancellationToken cancellationToken);
        Task<bool> DeleteUser(int userId, CancellationToken cancellationToken);
        Task<UserResponseDTO> GetUserByEmail(string email, CancellationToken cancellationToken);
        Task<List<UserResponseDTO>> GetUserByName(string keyword, CancellationToken cancellationToken);
    }
}
