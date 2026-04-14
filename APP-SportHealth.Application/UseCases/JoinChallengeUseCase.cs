using APP_SportHealth.Application.Interfaces;
using APP_SportHealth.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace APP_SportHealth.Application.UseCases
{
    public class JoinChallengeUseCase
    {
        private readonly IChallengeRepository _challengeRepository;

        public JoinChallengeUseCase(IChallengeRepository challengeRepository)
        {
            _challengeRepository = challengeRepository;
        }

        public async Task Execute(Guid userId, Guid challengeId)
        {
            var challenge = await _challengeRepository.GetById(challengeId);
            if (challenge == null) throw new ArgumentException("Challenge not found", nameof(challengeId));

            var existing = await _challengeRepository.GetUserChallenge(userId, challengeId);
            if (existing != null) throw new ArgumentException("User already joined this challenge");

            var uc = new UserChallenge(userId, challengeId);
            await _challengeRepository.AddUserChallenge(uc);
        }
    }
}
