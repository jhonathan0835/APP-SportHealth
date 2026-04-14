using System;

namespace APP_SportHealth.Domain.Entities
{
    public class UserChallenge
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid ChallengeId { get; private set; }
        public bool Completed { get; private set; }

        public UserChallenge(Guid userId, Guid challengeId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            ChallengeId = challengeId;
            Completed = false;
        }

        public void MarkCompleted() => Completed = true;
    }
}
