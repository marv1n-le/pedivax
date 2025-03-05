using Microsoft.AspNetCore.Mvc;
using PediVax.BusinessObjects.DTO.RequestDTO;
using PediVax.Services.IService;

namespace PediVax.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _authService.LoginAsync(loginRequest, cancellationToken);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
    
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        _authService.Logout();
        return Ok(new
        {
            message = "Logout successful"
        });
    }
}