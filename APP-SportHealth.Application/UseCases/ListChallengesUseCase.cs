using APP_SportHealth.Application.Interfaces;
using APP_SportHealth.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APP_SportHealth.Application.UseCases
{
    public class ListChallengesUseCase
    {
        private readonly IChallengeRepository _challengeRepository;

        public ListChallengesUseCase(IChallengeRepository challengeRepository)
        {
            _challengeRepository = challengeRepository;
        }

        public async Task<List<Challenge>> Execute()
        {
            return await _challengeRepository.ListAll();
        }
    }
}
