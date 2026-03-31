using APP_SportHealth.API.DTO;
using APP_SportHealth.API.Responses;
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
            await _createUserUseCase.Execute(
                request.Name,
                request.Email,
                request.Password
            );

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Usuario creado correctamente",
                Data = null
            });
        }
    }
}
