using AutoMapper;
using PediVax.BusinessObjects.DTO.ReponseDTO;
using PediVax.BusinessObjects.DTO.RequestDTO;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Repositories.Repository;
using PediVax.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.Services.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserResponseDTO>> GetAllUser()
        {
            var users = await _userRepository.GetAllUser();
            return _mapper.Map<List<UserResponseDTO>>(users);
        }

        public async Task<UserResponseDTO> CreateUser(CreateUserDTO createUserDTO)
        {
            var salt = GenerateSalt();
            var hashedPassword = HashPassword(createUserDTO.Password, salt);
            var user = _mapper.Map<User>(createUserDTO);
            user.PasswordHash = hashedPassword;
            user.PasswordSalt = salt;
            await _userRepository.AddUser(user);
            return _mapper.Map<UserResponseDTO>(user);

        }
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16]; // Tạo salt 16 byte
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes); // Tạo số ngẫu nhiên
            }
            return Convert.ToBase64String(saltBytes); // Chuyển đổi sang chuỗi base64
        }

        public static string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = password + salt; // Kết hợp mật khẩu và salt
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword)); // Hash chuỗi kết hợp
                return Convert.ToBase64String(hashedBytes); // Chuyển đổi sang chuỗi base64
            }
        }

        public async Task<(List<UserResponseDTO> Data, int TotalCount)> GetUserPaged(int pageNumber, int pageSize)
        {
            var (data, totalCount) = await _userRepository.GetUserPaged(pageNumber, pageSize);
            var mappedData = _mapper.Map<List<UserResponseDTO>>(data);
            return (mappedData, totalCount);
        }

        public async Task<UserResponseDTO> GetUserById(int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<bool> UpdateUser(int id, UpdateUserDTO updateUserDTO)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return false;
            }
            user = _mapper.Map(updateUserDTO, user);
            await _userRepository.UpdateUser(user);
            return true;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            return await _userRepository.DeleteUser(userId);
        }
    }
}
