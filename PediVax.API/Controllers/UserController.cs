using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PediVax.BusinessObjects.DTO.RequestDTO;
using PediVax.Services.IService;

namespace PediVax.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUser();
            if (users == null || users.Count == 0)
            {
                return NotFound("No users found");
            }
            return Ok(users);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserDTO createUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.CreateUser(createUserDTO);

            return Ok(user);
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("GetUsersPaged/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetUsersPaged(int pageNumber, int pageSize)
        {
            var (data, totalCount) = await _userService.GetUserPaged(pageNumber, pageSize);
            return Ok(new { Data = data, TotalCount = totalCount });
        }

        [HttpPut("UpdateUserById/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDTO updateUserDto)
        {
            var result = await _userService.UpdateUser(id, updateUserDto);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("DeleteUserById/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        
        [HttpGet("GetUserByEmail/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        
        [HttpGet("GetUserByName/{keyword}")]
        public async Task<IActionResult> GetUserByName(string keyword)
        {
            var users = await _userService.GetUserByName(keyword);
            if (users == null || users.Count == 0)
            {
                return NotFound("No users found");
            }
            return Ok(users);
        }
    }
}
