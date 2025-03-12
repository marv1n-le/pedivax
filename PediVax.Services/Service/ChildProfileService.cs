using System.Globalization;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using PediVax.BusinessObjects.DTO.ChildProfileDTO;
using PediVax.BusinessObjects.Enum;
using PediVax.BusinessObjects.Helpers;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Services.ExternalService;
using PediVax.Services.IService;

namespace PediVax.Services.Service;

public class ChildProfileService : IChildProfileService
{
    private readonly IChildProfileRepository _childProfileRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICloudinaryService _cloudinaryService;

    public ChildProfileService(IChildProfileRepository childProfileRepository, IMapper mapper,
        IHttpContextAccessor httpContextAccessor, ICloudinaryService cloudinaryService)
    {
        _childProfileRepository = childProfileRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _cloudinaryService = cloudinaryService;
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

    public async Task<List<ChildProfileResponseDTO>> GetAllChildProfiles()
    {
        var childProfiles = await _childProfileRepository.GetAllChildProfiles();

        return _mapper.Map<List<ChildProfileResponseDTO>>(childProfiles);
    }

    public async Task<ChildProfileResponseDTO> GetChildProfileById(int childProfileId)
    {
        var childProfile = await _childProfileRepository.GetChildProfileById(childProfileId);

        return _mapper.Map<ChildProfileResponseDTO>(childProfile);
    }

    public async Task<(List<ChildProfileResponseDTO> Data, int TotalCount)> GetChildProfilePaged(int pageNumber, int pageSize)
    {
        var (childProfiles, totalCount) = await _childProfileRepository.GetChildProfilePaged(pageNumber, pageSize);

        return (_mapper.Map<List<ChildProfileResponseDTO>>(childProfiles), totalCount);
    }

    public async Task<List<ChildProfileResponseDTO>> GetChildByName(string keyword)
    {
        var childProfile = await _childProfileRepository.GetChildByName(keyword);
        return _mapper.Map<List<ChildProfileResponseDTO>>(childProfile);
    }

    public async Task<ChildProfileResponseDTO> CreateChildProfile(CreateChildProfileDTO createChildProfileDTO)
    {
        try
        {
            DateTime birthDate = ParseDateHelper.ParseDate(createChildProfileDTO.DateOfBirth);

            var childProfile = _mapper.Map<ChildProfile>(createChildProfileDTO);
            if (createChildProfileDTO.ProfilePicture != null)
            {
                Console.WriteLine("Uploading image to Cloudinary...");
                childProfile.Image = await _cloudinaryService.UploadImage(createChildProfileDTO.ProfilePicture);
                Console.WriteLine("Image uploaded successfully.");
            }


            childProfile.UserId = createChildProfileDTO.UserId;
            childProfile.FullName = createChildProfileDTO.FullName;
            childProfile.DateOfBirth = birthDate;
            childProfile.Gender = createChildProfileDTO.Gender;
            childProfile.Relationship = createChildProfileDTO.Relationship;
            childProfile.IsActive = EnumList.IsActive.Active;
            childProfile.CreatedDate = DateTime.UtcNow;
            childProfile.CreatedBy = GetCurrentUserName();
            childProfile.ModifiedDate = DateTime.UtcNow;
            childProfile.ModifiedBy = GetCurrentUserName();
            if (await _childProfileRepository.CreateChildProfile(childProfile) <= 0)
            {
                throw new Exception("Error creating child profile");
            }

            return _mapper.Map<ChildProfileResponseDTO>(childProfile);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
            throw new Exception("Error while saving child profile: " + ex.InnerException?.Message, ex);
        }
    }

    public async Task<bool> UpdateChildProfile(int id, UpdateChildProfileDTO updateChildProfileDTO)
    {
        try
        {
            var existing = await _childProfileRepository.GetChildProfileById(id);
            if (existing == null)
            {
                throw new Exception("Child profile not found");
            }

            if (updateChildProfileDTO.ProfilePicture != null)
            {
                Console.WriteLine("Uploading updated image to Cloudinary...");
                existing.Image = await _cloudinaryService.UploadImage(updateChildProfileDTO.ProfilePicture);
                Console.WriteLine("Updated image uploaded successfully.");
            }

            existing.FullName = updateChildProfileDTO.FullName;
            existing.DateOfBirth = updateChildProfileDTO.DateOfBirth ?? existing.DateOfBirth;
            existing.Gender = updateChildProfileDTO.Gender ?? existing.Gender;
            existing.Relationship = updateChildProfileDTO.Relationship ?? existing.Relationship;
            existing.ModifiedDate = DateTime.UtcNow;
            existing.ModifiedBy = GetCurrentUserName();

            if (await _childProfileRepository.UpdateChildProfile(existing) <= 0)
            {
                throw new Exception("Error updating child profile");
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
            throw new Exception("Error while updating child profile: " + ex.InnerException?.Message, ex);
        }
    }


    public async Task<bool> DeleteChildProfile(int childProfileId)
    {
        return await _childProfileRepository.DeleteChildProfile(childProfileId);
    }
}