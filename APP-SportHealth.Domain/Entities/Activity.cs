using System;
using System.Collections.Generic;
using System.Text;

namespace APP_SportHealth.Domain.Entities
{
    public class Activity
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }

        public decimal Distance { get; private set; }
        public int Duration { get; private set; }

        public DateTime StartedAt { get; private set; }

        public Activity(Guid userId, decimal distance, int duration)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Distance = distance;
            Duration = duration;
            StartedAt = DateTime.UtcNow;
        }
    }
}
