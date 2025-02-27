using AutoMapper;
using PediVax.BusinessObjects.DTO.RequestDTO;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
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

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return _mapper.Map<List<User>>(users);
        }

        public async Task<User> AddUser(CreateUserDTO createUserDTO)
        {
            var salt = GenerateSalt();
            var hashedPassword = HashPassword(createUserDTO.Password, salt);
            var user = _mapper.Map<User>(createUserDTO);
            user.PasswordHash = hashedPassword;
            user.PasswordSalt = salt;
            return await _userRepository.AddUser(user);

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
    }
}
