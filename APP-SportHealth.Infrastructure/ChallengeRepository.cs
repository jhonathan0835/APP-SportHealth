using APP_SportHealth.Application.Interfaces;
using APP_SportHealth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace APP_SportHealth.Infrastructure
{
    public class ChallengeRepository : IChallengeRepository
    {
        private readonly AppDbContext _context;

        public ChallengeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Challenge challenge)
        {
            _context.Challenges.Add(challenge);
            await _context.SaveChangesAsync();
        }

        public async Task<Challenge?> GetById(Guid id)
        {
            return await _context.Challenges.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Challenge>> ListAll()
        {
            return await _context.Challenges.OrderByDescending(c => c.CreatedAt).ToListAsync();
        }

        public async Task<APP_SportHealth.Application.Models.PagedResult<Challenge>> ListPaged(int page, int pageSize, string orderBy, bool asc)
        {
            var query = _context.Challenges.AsQueryable();

            // ordering
            if (!string.IsNullOrEmpty(orderBy))
            {
                if (orderBy.ToLower() == "created_at" || orderBy.ToLower() == "createdat")
                    query = asc ? query.OrderBy(c => c.CreatedAt) : query.OrderByDescending(c => c.CreatedAt);
                else if (orderBy.ToLower() == "name")
                    query = asc ? query.OrderBy(c => c.Name) : query.OrderByDescending(c => c.Name);
                else if (orderBy.ToLower() == "target_distance" || orderBy.ToLower() == "targetdistance")
                    query = asc ? query.OrderBy(c => c.TargetDistance) : query.OrderByDescending(c => c.TargetDistance);
            }
            else
            {
                query = query.OrderByDescending(c => c.CreatedAt);
            }

            var total = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new APP_SportHealth.Application.Models.PagedResult<Challenge>
            {
                Items = items,
                Total = total,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task AddUserChallenge(UserChallenge userChallenge)
        {
            _context.UserChallenges.Add(userChallenge);
            await _context.SaveChangesAsync();
        }

        public async Task<UserChallenge?> GetUserChallenge(Guid userId, Guid challengeId)
        {
            return await _context.UserChallenges.FirstOrDefaultAsync(uc => uc.UserId == userId && uc.ChallengeId == challengeId);
        }

        public async Task MarkUserChallengeCompleted(Guid userChallengeId)
        {
            var uc = await _context.UserChallenges.FirstOrDefaultAsync(x => x.Id == userChallengeId);
            if (uc == null) return;
            uc.GetType().GetProperty("Completed")?.SetValue(uc, true);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserChallenge>> ListPendingUserChallenges()
        {
            return await _context.UserChallenges.Where(uc => uc.Completed == false).ToListAsync();
        }

        public async Task<List<UserChallenge>> ListUserChallengesByChallengeId(Guid challengeId)
        {
            return await _context.UserChallenges.Where(uc => uc.ChallengeId == challengeId).ToListAsync();
        }

        public async Task<List<(UserChallenge uc, User user)>> ListUserChallengesWithUser(Guid challengeId)
        {
            var query = from uc in _context.UserChallenges
                        join u in _context.Users on uc.UserId equals u.Id
                        where uc.ChallengeId == challengeId
                        select new { uc, u };

            var list = await query.ToListAsync();

            return list.Select(x => (x.uc, x.u)).ToList();
        }
    }
}
