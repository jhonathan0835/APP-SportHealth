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
        private readonly ListChallengeParticipantsUseCase _listParticipants;

        public ChallengesController(CreateChallengeUseCase createChallenge, JoinChallengeUseCase joinChallenge, ListChallengesUseCase listChallenges, ListChallengeParticipantsUseCase listParticipants)
        {
            _createChallenge = createChallenge;
            _joinChallenge = joinChallenge;
            _listChallenges = listChallenges;
            _listParticipants = listParticipants;
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
            var ucId = await _joinChallenge.Execute(request.UserId, id);
            if (ucId == Guid.Empty)
                return NotFound(new { success = false, message = "Challenge not found" });

            return Ok(new { success = true, userChallengeId = ucId });
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string orderBy = "created_at", [FromQuery] bool asc = false)
        {
            var challenges = await _listChallenges.Execute(page, pageSize, orderBy, asc);
            return Ok(challenges);
        }

        [HttpGet("{id}/participants")]
        public async Task<IActionResult> Participants([FromRoute] Guid id)
        {
            var participants = await _listParticipants.Execute(id);
            return Ok(participants);
        }
    }
}
