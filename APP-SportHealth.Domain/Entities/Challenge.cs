using System;

namespace APP_SportHealth.Domain.Entities
{
    public class Challenge
    {
        public Guid Id { get; private set; }
        public decimal TargetDistance { get; private set; }
        public int? TimeLimit { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Name { get; private set; }

        public Challenge(string name, decimal targetDistance, int? timeLimit)
        {
            Id = Guid.NewGuid();
            Name = name;
            TargetDistance = targetDistance;
            TimeLimit = timeLimit;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
