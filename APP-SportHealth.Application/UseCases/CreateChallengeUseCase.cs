using APP_SportHealth.Application.Interfaces;
using APP_SportHealth.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace APP_SportHealth.Application.UseCases
{
    public class CreateChallengeUseCase
    {
        private readonly IChallengeRepository _challengeRepository;

        public CreateChallengeUseCase(IChallengeRepository challengeRepository)
        {
            _challengeRepository = challengeRepository;
        }

        public async Task<Guid> Execute(string name, decimal targetDistance, int? timeLimit)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required", nameof(name));
            if (targetDistance <= 0) throw new ArgumentException("Target distance must be > 0", nameof(targetDistance));

            var challenge = new Challenge(name, targetDistance, timeLimit);
            await _challengeRepository.Create(challenge);
            return challenge.Id;
        }
    }
}
