using APP_SportHealth.API.DTO;
using APP_SportHealth.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace APP_SportHealth.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChallengesController : ControllerBase
    {
        private readonly CreateChallengeUseCase _createChallenge;
        private readonly JoinChallengeUseCase _joinChallenge;
        private readonly ListChallengesUseCase _listChallenges;

        public ChallengesController(CreateChallengeUseCase createChallenge, JoinChallengeUseCase joinChallenge, ListChallengesUseCase listChallenges)
        {
            _createChallenge = createChallenge;
            _joinChallenge = joinChallenge;
            _listChallenges = listChallenges;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateChallengeRequest request)
        {
            var id = await _createChallenge.Execute(request.Name, request.TargetDistance, request.TimeLimit);
            return Ok(new { success = true, id });
        }

        [HttpPost("{id}/join")]
        public async Task<IActionResult> Join([FromRoute] Guid id, [FromBody] JoinChallengeRequest request)
        {
            await _joinChallenge.Execute(request.UserId, id);
            return Ok(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var challenges = await _listChallenges.Execute();
            return Ok(challenges);
        }
    }
}
