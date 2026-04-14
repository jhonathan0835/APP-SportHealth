using APP_SportHealth.Application.Interfaces;
using APP_SportHealth.Application.Models;
using APP_SportHealth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APP_SportHealth.Application.UseCases
{
    public class ListChallengeParticipantsUseCase
    {
        private readonly IChallengeRepository _challengeRepository;

        public ListChallengeParticipantsUseCase(IChallengeRepository challengeRepository)
        {
            _challengeRepository = challengeRepository;
        }

        public async Task<List<ParticipantInfo>> Execute(Guid challengeId)
        {
            var list = await _challengeRepository.ListUserChallengesWithUser(challengeId);

            return list.Select(x => new ParticipantInfo
            {
                UserId = x.user.Id,
                Name = x.user.Name,
                Email = x.user.Email,
                Completed = x.uc.Completed,
                UserChallengeId = x.uc.Id
            }).ToList();
        }
    }
}
