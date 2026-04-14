using System;

namespace APP_SportHealth.API.DTO
{
    public class CreateChallengeRequest
    {
        public string Name { get; set; }
        public decimal TargetDistance { get; set; }
        public int? TimeLimit { get; set; }
    }

    public class JoinChallengeRequest
    {
        public Guid UserId { get; set; }
    }
}
