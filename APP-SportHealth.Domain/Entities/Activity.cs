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

        public decimal? Calories { get; private set; }
        public decimal? AvgPace { get; private set; }

        public DateTime StartedAt { get; private set; }
        public DateTime? EndedAt { get; private set; }

        public List<ActivityPoint> Points { get; private set; } = new List<ActivityPoint>();

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
