using APP_SportHealth.Application.Interfaces;
using APP_SportHealth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APP_SportHealth.Application.UseCases
{
    // This use case evaluates pending user_challenges and marks them as completed
    public class EvaluateChallengesUseCase
    {
        private readonly IChallengeRepository _challengeRepository;
        private readonly IActivityRepository _activityRepository;

        public EvaluateChallengesUseCase(IChallengeRepository challengeRepository, IActivityRepository activityRepository)
        {
            _challengeRepository = challengeRepository;
            _activityRepository = activityRepository;
        }

        public async Task Execute()
        {
            // 1. get pending user challenges
            var pending = await _challengeRepository.ListPendingUserChallenges();

            foreach (var uc in pending)
            {
                try
                {
                    var challenge = await _challengeRepository.GetById(uc.ChallengeId);
                    if (challenge == null) continue;

                    // sum user activities distance (naive: all time) - replace with time-limited if needed
                    var activities = await _activityRepository.ListByUser(uc.UserId);
                    var totalDistance = activities.Sum(a => a.Distance);

                    if (totalDistance >= challenge.TargetDistance)
                    {
                        await _challengeRepository.MarkUserChallengeCompleted(uc.Id);
                    }
                }
                catch
                {
                    // swallow per-item errors to continue processing others
                }
            }
        }
    }
}
