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
        private readonly APP_SportHealth.Application.Interfaces.IPaginationOptionsProvider _paginationOptionsProvider;

        public ListChallengesUseCase(IChallengeRepository challengeRepository, APP_SportHealth.Application.Interfaces.IPaginationOptionsProvider paginationOptionsProvider)
        {
            _challengeRepository = challengeRepository;
            _paginationOptionsProvider = paginationOptionsProvider;
        }

        public async Task<PagedResult<Challenge>> Execute(int page = 1, int pageSize = 10, string orderBy = "created_at", bool asc = false)
        {
            if (pageSize <= 0) pageSize = 1;
            var max = _paginationOptionsProvider.GetOptions()?.MaxPageSize ?? 10;
            pageSize = System.Math.Min(pageSize, max);

            var result = await _challengeRepository.ListPaged(page, pageSize, orderBy, asc);
            // compute total pages
            result.TotalPages = (int)System.Math.Ceiling((double)result.Total / (double)(result.PageSize <= 0 ? 1 : result.PageSize));

            return result;
        }
    }
}
