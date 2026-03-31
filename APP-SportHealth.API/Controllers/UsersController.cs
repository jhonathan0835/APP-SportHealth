using APP_SportHealth.API.DTO;
using APP_SportHealth.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace APP_SportHealth.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly CreateUserUseCase _createUserUseCase;

        public UsersController(CreateUserUseCase createUserUseCase)
        {
            _createUserUseCase = createUserUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {
            try
            {
                await _createUserUseCase.Execute(
                    request.Name,
                    request.Email,
                    request.Password
                );

                return Ok(new { message = "Usuario creado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
