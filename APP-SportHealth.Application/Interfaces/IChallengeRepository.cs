using APP_SportHealth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APP_SportHealth.Application.Interfaces
{
    public interface IChallengeRepository
    {
        Task Create(Challenge challenge);
        Task<Challenge?> GetById(Guid id);
        Task<List<Challenge>> ListAll();
        Task AddUserChallenge(UserChallenge userChallenge);
        Task<UserChallenge?> GetUserChallenge(Guid userId, Guid challengeId);
        Task MarkUserChallengeCompleted(Guid userChallengeId);
        Task<List<UserChallenge>> ListPendingUserChallenges();
    }
}
