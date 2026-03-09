using Microsoft.AspNetCore.Mvc;
using Todo_List_ASP.DTOs;
using Todo_List_ASP.Models;
using Todo_List_ASP.Services;

namespace Todo_List_ASP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("signup")]
        public async Task<ActionResult<User>> SignUp([FromBody] CreateUserRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Username) ||
                    string.IsNullOrWhiteSpace(request.Email) ||
                    string.IsNullOrWhiteSpace(request.Password))
                {
                    return BadRequest("All fields are required.");
                }

                var createdUser = await _service.CreateUserAsync(request);

                return Ok(createdUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Username) ||
                    string.IsNullOrWhiteSpace(request.Password))
                    return BadRequest("All fields are required.");

                var response = await _service.LoginAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message); // 401, not 400
            }
        }
    }
}