using PediVax.BusinessObjects.DTO.RequestDTO;
using PediVax.BusinessObjects.DTO.ResponseDTO;
using PediVax.BusinessObjects.Models;

namespace PediVax.Services.IService;

public interface IAuthService
{
    Task<AuthResponseDTO> LoginAsync(LoginRequestDTO loginRequestDto, CancellationToken cancellationToken);
}