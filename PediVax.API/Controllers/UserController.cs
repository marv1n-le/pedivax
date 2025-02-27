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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsers();
            if (users == null || users.Count == 0)
            {
                return NotFound("No users found");
            }
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDTO createUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.AddUser(createUserDTO);

            return Ok(user);
        }
    }
}
