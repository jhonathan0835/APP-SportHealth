using APP_SportHealth.Application.Interfaces;
using APP_SportHealth.Domain.Entities;
using APP_SportHealth.Application.Exceptions;
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

        public async Task<Guid> Execute(Guid userId, Guid challengeId)
        {
            var challenge = await _challengeRepository.GetById(challengeId);
            if (challenge == null)
                return Guid.Empty;

            var existing = await _challengeRepository.GetUserChallenge(userId, challengeId);
            if (existing != null) throw new BusinessException("User already joined this challenge");

            var uc = new UserChallenge(userId, challengeId);
            await _challengeRepository.AddUserChallenge(uc);
            return uc.Id;
        }
    }
}
