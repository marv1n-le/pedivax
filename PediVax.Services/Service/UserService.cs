using AutoMapper;
using PediVax.BusinessObjects.DTO.ReponseDTO;
using PediVax.BusinessObjects.DTO.RequestDTO;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Repositories.Repository;
using PediVax.Services.IService;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PediVax.BusinessObjects.Enum;
using PediVax.BusinessObjects.Helpers;

namespace PediVax.Services.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
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

        public async Task<List<UserResponseDTO>> GetAllUser()
        {
            var users = await _userRepository.GetAllUser();
            return _mapper.Map<List<UserResponseDTO>>(users);
        }

        public async Task<UserResponseDTO> CreateUser(CreateUserDTO createUserDTO)
        {
            try
            {
                ValidateInputs(createUserDTO);
                if (await CheckPhoneExist(createUserDTO.PhoneNumber))
                    throw new Exception("Phone number already exists.");
                if (await CheckEmailExist(createUserDTO.Email))
                    throw new Exception("Email already exists.");
                var salt = PasswordHelper.GenerateSalt();
                var hashedPassword = PasswordHelper.HashPassword(createUserDTO.Password, salt);

                var user = new User()
                {
                    FullName = createUserDTO.FullName,
                    Email = createUserDTO.Email,
                    PhoneNumber = createUserDTO.PhoneNumber,
                    PasswordHash = hashedPassword,
                    PasswordSalt = salt,
                    Address = createUserDTO.Address,
                    Role = EnumList.Role.Customer,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = GetCurrentUserName(),
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = GetCurrentUserName(),
                    IsActive = EnumList.IsActive.Active
                };
                if (await _userRepository.AddUser(user) <= 0)
                {
                    throw new Exception("Failed to add user");
                }
                return _mapper.Map<UserResponseDTO>(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while saving user: " + ex.InnerException?.Message, ex);
            }
        }
        
        public async Task<UserResponseDTO> CreateSystemUser(CreateSystemUserDTO createSystemUserDto)
        {
            try
            {
                if (createSystemUserDto.Role != EnumList.Role.Staff && createSystemUserDto.Role != EnumList.Role.Doctor)
                {
                    throw new Exception("Invalid role. Only Staff (2) and Doctor (3) can be created.");
                }
                ValidateInputs(createSystemUserDto);
                if (await CheckPhoneExist(createSystemUserDto.PhoneNumber))
                    throw new Exception("Phone number already exists.");
                if (await CheckEmailExist(createSystemUserDto.Email))
                    throw new Exception("Email already exists.");
                var salt = PasswordHelper.GenerateSalt();
                var hashedPassword = PasswordHelper.HashPassword(createSystemUserDto.Password, salt);

                var user = new User()
                {
                    FullName = createSystemUserDto.FullName,
                    Email = createSystemUserDto.Email,
                    PhoneNumber = createSystemUserDto.PhoneNumber,
                    PasswordHash = hashedPassword,
                    PasswordSalt = salt,
                    Address = createSystemUserDto.Address,
                    Role = createSystemUserDto.Role,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = GetCurrentUserName(),
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = GetCurrentUserName(),
                    IsActive = EnumList.IsActive.Active
                };
                if (await _userRepository.AddUser(user) <= 0)
                {
                    throw new Exception("Failed to add user");
                }
                return _mapper.Map<UserResponseDTO>(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while saving user: " + ex.InnerException?.Message, ex);
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
            if (updateUserDTO.Role != EnumList.Role.Staff && updateUserDTO.Role != EnumList.Role.Doctor
                && updateUserDTO.Role != EnumList.Role.Customer)
            {
                throw new Exception("Invalid role. Only Staff (2), Doctor (3) and Customer (1) can be updated.");
            }
            if (updateUserDTO == null) throw new ArgumentNullException(nameof(updateUserDTO));
            try
            {
                ValidateUpdateInputs(updateUserDTO);
                var existing = await _userRepository.GetUserById(id);
                if (existing == null)
                {
                    throw new Exception("User not found.");
                }
                if (await CheckPhoneExist(updateUserDTO.PhoneNumber) && existing.PhoneNumber != updateUserDTO.PhoneNumber)
                    throw new Exception("Phone number already exists.");
                if (await CheckEmailExist(updateUserDTO.Email) && existing.Email != updateUserDTO.Email)
                    throw new Exception("Email already exists.");
                
                existing.FullName = updateUserDTO.FullName;
                existing.Email = updateUserDTO.Email;
                existing.PhoneNumber = updateUserDTO.PhoneNumber;
                existing.Role = updateUserDTO.Role;
                existing.Address = updateUserDTO.Address;
                existing.ModifiedDate = DateTime.UtcNow;
                existing.ModifiedBy = GetCurrentUserName();
                if (await _userRepository.UpdateUser(existing) <= 0)
                {
                    throw new Exception("Failed to update user");
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating user: " + ex.InnerException?.Message, ex);;
            }
        }

        public async Task<bool> DeleteUser(int userId)
        {
            return await _userRepository.DeleteUser(userId);
        }
        
        public async Task<UserResponseDTO> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return _mapper.Map<UserResponseDTO>(user);
        }
        
        public async Task<List<UserResponseDTO>> GetUserByName(string keyword)
        {
            var users = await _userRepository.GetUserByName(keyword);
            return _mapper.Map<List<UserResponseDTO>>(users);
        }
        
        //VALIDATE USER
        
        private async Task<bool> CheckPhoneExist(string phone)
        {
            var users = await _userRepository.GetAllUser();
            return users.Any(u => u.PhoneNumber == phone);
        }
        
        private async Task<bool> CheckEmailExist(string email)
        {
            var users = await _userRepository.GetAllUser();
            return users.Any(u => u.Email == email);
        }
        private void ValidateInputs(CreateUserDTO createUserDTO)
        {
            ValidateCommonInputs(createUserDTO.PhoneNumber, createUserDTO.Email, createUserDTO.FullName, createUserDTO.Password);
        }
        
        private void ValidateInputs(CreateSystemUserDTO createSystemUserDTO)
        {
            ValidateCommonInputs(createSystemUserDTO.PhoneNumber, createSystemUserDTO.Email, createSystemUserDTO.FullName, createSystemUserDTO.Password);
        }
        
        private void ValidateUpdateInputs(UpdateUserDTO updateUserDTO)
        {
            ValidatePhoneNumber(updateUserDTO.PhoneNumber);
            ValidateEmail(updateUserDTO.Email);
            ValidateFullName(updateUserDTO.FullName);
        }
        private void ValidateCommonInputs(string phone, string email, string fullName, string password)
        {
            ValidatePhoneNumber(phone);
            ValidateEmail(email);
            ValidateFullName(fullName);
            ValidatePassword(password);
        }
        private void ValidatePhoneNumber(string phone)
        {
            if (!Regex.IsMatch(phone, @"^0\d{9}$"))
                throw new ArgumentException("Phone number must start with 0 and contain exactly 10 digits.");
        }
        private void ValidateEmail(string email)
        {
            if (!Regex.IsMatch(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                throw new ArgumentException("Invalid email format.");
        }
        private void ValidateFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName) || fullName.Length > 100)
                throw new Exception("Full name cannot be empty and must be under 100 characters.");
        }
        private void ValidatePassword(string password)
        {
            if (password.Length < 8 || !Regex.IsMatch(password, @"[A-Z]") || !Regex.IsMatch(password, @"[a-z]") ||
                !Regex.IsMatch(password, @"\d") || !Regex.IsMatch(password, @"[\W_]"))
                throw new Exception("Password must be at least 6 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
        }
    }
}
