using Microsoft.AspNetCore.Mvc;
using Quantaco.Models;

namespace Quantaco.Api.Controllers
{
    
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : Controller
    {
        private readonly Services.Interfaces.ILoginService _loginService;

        public AuthenticationController(Services.Interfaces.ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<TeacherAuthResponseModel>> Register(TeacherRegistrationModel registration)
        {
            try
            {
                var response = await _loginService.RegisterAsync(registration);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<TeacherAuthResponseModel>> Login(TeacherLoginModel login)
        {
            try
            {
                var response = await _loginService.LoginAsync(login);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}
