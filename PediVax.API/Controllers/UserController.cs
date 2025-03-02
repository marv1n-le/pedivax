using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PediVax.BusinessObjects.DTO.RequestDTO;
using PediVax.Services.IService;

namespace PediVax.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet("current-user")]
        public IActionResult GetCurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userId == null)
                return Unauthorized("User is not logged in.");

            return Ok(new
            {
                UserId = userId,
                Email = email,
                Role = role
            });
        }

        [HttpGet("get-all-users")]
        [Authorize(Roles = "Admin, Staff, Doctor")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUser();
            if (users == null || users.Count == 0)
            {
                return NotFound("No users found");
            }
            return Ok(users);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]CreateUserDTO createUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.CreateUser(createUserDTO);

            return Ok(user);
        }

        [HttpGet("get-user-by-id/{id}")]
        [Authorize(Roles = "Admin, Staff, Doctor")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("get-user-paged/{pageNumber}/{pageSize}")]
        [Authorize(Roles = "Admin, Staff, Doctor")]
        public async Task<IActionResult> GetUsersPaged(int pageNumber, int pageSize)
        {
            var (data, totalCount) = await _userService.GetUserPaged(pageNumber, pageSize);
            return Ok(new { Data = data, TotalCount = totalCount });
        }

        [HttpPut("update-user-by-id/{id}")]
        [Authorize(Roles = "Admin, Staff, Doctor")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDTO updateUserDto)
        {
            var result = await _userService.UpdateUser(id, updateUserDto);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("delete-user-by-id/{id}")]
        [Authorize(Roles = "Admin, Staff, Doctor")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        
        [HttpGet("get-user-by-email/{email}")]
        [Authorize(Roles = "Admin, Staff, Doctor")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        
        [HttpGet("get-user-by-name/{keyword}")]
        [Authorize(Roles = "Admin, Staff, Doctor")]
        public async Task<IActionResult> GetUserByName(string keyword)
        {
            var users = await _userService.GetUserByName(keyword);
            if (users == null || users.Count == 0)
            {
                return NotFound("No users found");
            }
            return Ok(users);
        }
        
        [HttpPost("create-system-user")]
        [Authorize(Roles = "Admin, Staff, Doctor")]
        public async Task<IActionResult> CreateSystemUser([FromBody] CreateSystemUserDTO createSystemUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.CreateSystemUser(createSystemUserDTO);

            return Ok(user);
        }
        
    }
}
