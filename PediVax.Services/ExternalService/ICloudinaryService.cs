using Microsoft.AspNetCore.Http;

namespace PediVax.Services.ExternalService;

public interface ICloudinaryService
{
    Task<string> UploadImage(IFormFile file);
}