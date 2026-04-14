using APP_SportHealth.API.DTO;
using APP_SportHealth.Application.UseCases;
using APP_SportHealth.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace APP_SportHealth.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivitiesController : ControllerBase
    {
        private readonly CreateActivityUseCase _createActivity;

        public ActivitiesController(CreateActivityUseCase createActivity)
        {
            _createActivity = createActivity;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateActivityRequest request)
        {
            var points = request.Points?.Select(p => (p.Latitude, p.Longitude, p.Timestamp)).ToList();

            await _createActivity.Execute(request.UserId, request.Distance, request.Duration, points);

            return Ok(new { success = true });
        }
    }
}
