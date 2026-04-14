using APP_SportHealth.Application.Interfaces;
using APP_SportHealth.Application.Models;
using APP_SportHealth.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<PagedResult<Challenge>> Execute(int page = 1, int pageSize = 10, string orderBy = "created_at", bool asc = false)
        {
            return await _challengeRepository.ListPaged(page, pageSize, orderBy, asc);
        }
    }
}
