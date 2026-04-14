using System;

namespace APP_SportHealth.Application.Models
{
    public class ParticipantInfo
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Completed { get; set; }
        public Guid UserChallengeId { get; set; }
    }
}
