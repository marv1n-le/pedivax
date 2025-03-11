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
using PediVax.Services.ExternalService;
using Microsoft.Extensions.Logging;
using System.Transactions;
using System.Threading;

namespace PediVax.Services.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, ICloudinaryService cloudinaryService, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _cloudinaryService = cloudinaryService;
            _logger = logger;
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

        public async Task<List<UserResponseDTO>> GetAllUser(CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUser(cancellationToken);
            return _mapper.Map<List<UserResponseDTO>>(users);
        }


        public async Task<UserResponseDTO> CreateUser(CreateUserDTO createUserDTO, CancellationToken cancellationToken)
        {
            if (createUserDTO == null)
            {
                throw new ArgumentNullException(nameof(createUserDTO), "User data is required.");
            }
            try
            {
                ValidateInputs(createUserDTO);
                if (await CheckPhoneExist(createUserDTO.PhoneNumber, cancellationToken))
                    throw new Exception("Phone number already exists.");
                if (await CheckEmailExist(createUserDTO.Email, cancellationToken))
                    throw new Exception("Email already exists.");
                if (createUserDTO.Image == null || createUserDTO.Image.Length == 0)
                    throw new ArgumentException("Invalid vaccine image");

                using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

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
                    Image = await _cloudinaryService.UploadImage(createUserDTO.Image),
                    Role = EnumList.Role.Customer,
                    DateOfBirth = createUserDTO.DateOfBirth,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = GetCurrentUserName(),
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = GetCurrentUserName(),
                    IsActive = EnumList.IsActive.Active
                    
                };
                if (await _userRepository.AddUser(user, cancellationToken) <= 0)
                {
                    throw new Exception("Failed to add user");
                }

                scope.Complete();
                return _mapper.Map<UserResponseDTO>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding new vaccine");
                throw new ApplicationException("Error while saving user: " + ex.InnerException?.Message, ex);
            }
        }
        
        public async Task<UserResponseDTO> CreateSystemUser(CreateSystemUserDTO createSystemUserDto, CancellationToken cancellationToken)
        {
            if (createSystemUserDto == null)
            {
                throw new ArgumentNullException(nameof(createSystemUserDto), "User data is required.");
            }
            try
            {
                if (createSystemUserDto.Role != EnumList.Role.Staff && createSystemUserDto.Role != EnumList.Role.Doctor)
                {
                    throw new Exception("Invalid role. Only Staff (2) and Doctor (3) can be created.");
                }
                ValidateInputs(createSystemUserDto);
                if (await CheckPhoneExist(createSystemUserDto.PhoneNumber, cancellationToken))
                    throw new Exception("Phone number already exists.");
                if (await CheckEmailExist(createSystemUserDto.Email, cancellationToken))
                    throw new Exception("Email already exists.");

                var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

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
                    Image = await _cloudinaryService.UploadImage(createSystemUserDto.Image),
                    Role = createSystemUserDto.Role,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = GetCurrentUserName(),
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = GetCurrentUserName(),
                    IsActive = EnumList.IsActive.Active
                };
                if (await _userRepository.AddUser(user, cancellationToken) <= 0)
                {
                    throw new Exception("Failed to add user");
                }

                scope.Complete();
                return _mapper.Map<UserResponseDTO>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding new system user !");
                throw new ApplicationException("Error while saving user: " + ex.InnerException?.Message, ex);
            }
        }

        public async Task<(List<UserResponseDTO> Data, int TotalCount)> GetUserPaged(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                _logger.LogWarning("Invalid pagination parameters: PageNumber={pageNumber}, PageSize={pageSize}", pageNumber, pageSize);
                throw new ArgumentException("Invalid pagination parameters");
            }
            var (data, totalCount) = await _userRepository.GetUserPaged(pageNumber, pageSize, cancellationToken);
            var mappedData = _mapper.Map<List<UserResponseDTO>>(data);
            return (mappedData, totalCount);
        }

        public async Task<UserResponseDTO> GetUserById(int userId, CancellationToken cancellationToken)
        {
            if (userId <= 0)
            {
                _logger.LogWarning("Invalid User ID: {userId}", userId);
                throw new ArgumentException("Invalid user id.");
            }
            var user = await _userRepository.GetUserById(userId, cancellationToken);

            if (user == null)
            {
                _logger.LogWarning("User with ID {userId} not found", userId);
                throw new KeyNotFoundException("User not found");
            }
            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<bool> UpdateUser(int id, UpdateUserDTO updateUserDTO, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid user ID");
            }

            if (updateUserDTO == null)
            {
                throw new ArgumentNullException(nameof(updateUserDTO), "User data is required.");
            }

            if (updateUserDTO.Role != null && updateUserDTO.Role != EnumList.Role.Staff &&
                updateUserDTO.Role != EnumList.Role.Doctor && updateUserDTO.Role != EnumList.Role.Customer)
            {
                throw new Exception("Invalid role. Only Staff (2), Doctor (3) and Customer (1) can be updated.");
            }

            try
            {
                var existing = await _userRepository.GetUserById(id, cancellationToken);
                if (existing == null)
                {
                    _logger.LogWarning("User with ID {userId} not found", id);
                    throw new KeyNotFoundException("User not found.");
                }

                if (!string.IsNullOrEmpty(updateUserDTO.PhoneNumber))
                {
                    if (!Regex.IsMatch(updateUserDTO.PhoneNumber, @"^0\d{9}$"))
                    {
                        throw new ArgumentException("Phone number must start with 0 and contain exactly 10 digits.");
                    }
                    if (await CheckPhoneExist(updateUserDTO.PhoneNumber, cancellationToken) && existing.PhoneNumber != updateUserDTO.PhoneNumber)
                    {
                        throw new Exception("Phone number already exists.");
                    }
                    existing.PhoneNumber = updateUserDTO.PhoneNumber;
                }

                if (!string.IsNullOrEmpty(updateUserDTO.Email))
                {
                    if (!Regex.IsMatch(updateUserDTO.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                    {
                        throw new ArgumentException("Invalid email format.");
                    }
                    if (await CheckEmailExist(updateUserDTO.Email, cancellationToken) && existing.Email != updateUserDTO.Email)
                    {
                        throw new Exception("Email already exists.");
                    }
                    existing.Email = updateUserDTO.Email;
                }

                if (!string.IsNullOrWhiteSpace(updateUserDTO.FullName))
                {
                    if (updateUserDTO.FullName.Length > 100)
                    {
                        throw new Exception("Full name must be under 100 characters.");
                    }
                    existing.FullName = updateUserDTO.FullName;
                }

                existing.Role = updateUserDTO.Role ?? existing.Role;
                existing.Address = !string.IsNullOrWhiteSpace(updateUserDTO.Address) ? updateUserDTO.Address : existing.Address;
                existing.ModifiedDate = DateTime.UtcNow;
                existing.ModifiedBy = GetCurrentUserName();

                if (updateUserDTO.Image != null && updateUserDTO.Image.Length > 0)
                {
                    existing.Image = await _cloudinaryService.UploadImage(updateUserDTO.Image);
                }
                int rowAffected = await _userRepository.UpdateUser(existing, cancellationToken);
                return rowAffected > 0;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user with ID {id} !", id);
                throw new ApplicationException("Error while updating user: " + ex.InnerException?.Message, ex);
            }
        }

        public async Task<bool> DeleteUser(int userId, CancellationToken cancellationToken)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid user ID !");
            }
            try
            {
                return await _userRepository.DeleteUser(userId, cancellationToken);
            }
                catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user with ID {userId}", userId);
                throw new ApplicationException("Error while deleting vaccine", ex);
            }
        }
        
        public async Task<UserResponseDTO> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be empty.");
            }
            var user = await _userRepository.GetByEmailAsync(email, cancellationToken);
            return _mapper.Map<UserResponseDTO>(user);
        }
        
        public async Task<List<UserResponseDTO>> GetUserByName(string keyword, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                throw new ArgumentException("Keyword cannot be empty.");
            }
            var users = await _userRepository.GetUserByName(keyword, cancellationToken);
            return _mapper.Map<List<UserResponseDTO>>(users);
        }
        
        //VALIDATE USER
        
        private async Task<bool> CheckPhoneExist(string phone, CancellationToken cancellationToken)
        {

            var users = await _userRepository.GetAllUser(cancellationToken);
            return users.Any(u => u.PhoneNumber == phone);
        }
        
        private async Task<bool> CheckEmailExist(string email, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUser(cancellationToken);
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
