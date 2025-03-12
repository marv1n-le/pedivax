using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PediVax.BusinessObjects.DTO.ReponseDTO;
using PediVax.BusinessObjects.DTO.RequestDTO;
using PediVax.Services.IService;

namespace PediVax.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("current-user")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IActionResult GetCurrentUser()
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return Unauthorized("User is not logged in.");
            }

            var userIdClaim = User.Claims
                .Where(c => c.Type == ClaimTypes.NameIdentifier && int.TryParse(c.Value, out _))
                .FirstOrDefault()?.Value;

            var email = User.FindFirst(ClaimTypes.Email)?.Value ?? "Unknown";
            var role = User.FindFirst(ClaimTypes.Role)?.Value ?? "Unknown";

            return Ok(new
            {
                UserId = userIdClaim ?? "Unknown",
                Email = email,
                Role = role
            });
        }

        [HttpGet("get-all-users")]
        [ProducesResponseType(typeof(List<UserResponseDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Authorize(Roles = "Admin, Staff, Doctor, Customer")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return Unauthorized("Unauthorized user!!!.");
            }
            var response = await _userService.GetAllUser(cancellationToken);
            if (response == null || response.Count == 0)
            {
                _logger.LogWarning("No users found");
                return NotFound(new {message = "No users available."});
            }
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm]CreateUserDTO createUserDTO, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.CreateUser(createUserDTO, cancellationToken);

            return Ok(user);
        }

        [HttpGet("get-user-by-id/{id}")]
        [ProducesResponseType(typeof(UserResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Authorize(Roles = "Admin, Staff, Doctor, Customer")]
        public async Task<IActionResult> GetUserById(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid user ID." });
            }
            try
            {
                var user = await _userService.GetUserById(id, cancellationToken);
                return Ok(user);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "User not found" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user with ID {id}", id);
                return Problem("An unexpected error occurred.");
            }
            
        }

        [HttpGet("get-user-paged/{pageNumber}/{pageSize}")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Authorize(Roles = "Admin, Staff, Doctor")]
        public async Task<IActionResult> GetUsersPaged(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest(new { message = "Invalid pagination parameters" });
            }
            var (data, totalCount) = await _userService.GetUserPaged(pageNumber, pageSize, cancellationToken);
            return Ok(new { Data = data, TotalCount = totalCount });
        }

        [HttpPut("update-user-by-id/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Authorize(Roles = "Admin, Staff, Doctor")]
        public async Task<IActionResult> UpdateUser(int id, [FromForm] UpdateUserDTO updateUserDto, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid user ID." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data.", errors = ModelState.Values });
            }
            try
            {
                var response = await _userService.UpdateUser(id, updateUserDto, cancellationToken);
                return Ok(new { success = response});
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "User not found" });
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error updating user with ID {id}", id);
                return Problem("An error occurred while updating the user !");
            }
            
        }

        [HttpDelete("delete-user-by-id/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]

        [Authorize(Roles = "Admin, Staff, Doctor")]
        public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    message = "Invalid user ID."
                });
            }

            try
            {
                var result = await _userService.DeleteUser(id, cancellationToken);
                if (!result)
                {
                    return NotFound(new
                    {
                        message = "User not found or already deleted !"
                    });
                }

                return Ok(new
                {
                    message = "User deleted successfully !"
                });
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error deleting user with ID {id}", id);
                return Problem("An error occurred while deleting the user !");
            }
            
        }
        
        [HttpGet("get-user-by-email/{email}")]
        [ProducesResponseType(typeof(UserResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]

        [Authorize(Roles = "Admin, Staff, Doctor")]
        public async Task<IActionResult> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest(new
                {
                    message = "Email cannot be empty!"
                });
            }
            var user = await _userService.GetUserByEmail(email, cancellationToken);
            if (user == null)
            {
                return NotFound("No users found with the given email !");
            }
            return Ok(user);
        }
        
        [HttpGet("get-user-by-name/{keyword}")]
        [ProducesResponseType(typeof(List<UserResponseDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]

        [Authorize(Roles = "Admin, Staff, Doctor")]
        public async Task<IActionResult> GetUserByName(string keyword, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(keyword)  )
            {
                return BadRequest(new
                {
                    message = "Keyword cannot be empty!"
                });
            }
            var users = await _userService.GetUserByName(keyword, cancellationToken);
            if (users == null || users.Count == 0)
            {
                return NotFound("No users found with the give keyword !");
            }
            return Ok(users);
        }
        
        [HttpPost("create-system-user")]
        [ProducesResponseType(typeof(UserResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]

        [Authorize(Roles = "Admin, Staff, Doctor")]
        public async Task<IActionResult> CreateSystemUser([FromForm] CreateSystemUserDTO createSystemUserDTO, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Invalid data.", errors = ModelState.Values
                });
            }
            
            try
            {
                var response = await _userService.CreateSystemUser(createSystemUserDTO, cancellationToken);
                return CreatedAtAction(nameof(GetUserById), new { id = response.UserId }, response);
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error creating a new system user.");
                return Problem("An error occurred while creating the user !");
            }
        }
        
    }
}
