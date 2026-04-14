using APP_SportHealth.Application.UseCases;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace APP_SportHealth.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly LoginUseCase _login;

        public AuthController(LoginUseCase login)
        {
            _login = login;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var token = await _login.Execute(request.Email, request.Password);

            if (token == null)
                return Unauthorized();

            return Ok(new { token });
        }
    }
}
