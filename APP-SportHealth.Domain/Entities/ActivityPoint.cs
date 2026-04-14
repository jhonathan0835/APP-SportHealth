using System;

namespace APP_SportHealth.Domain.Entities
{
    public class ActivityPoint
    {
        public Guid Id { get; private set; }
        public Guid ActivityId { get; private set; }

        public decimal Latitude { get; private set; }
        public decimal Longitude { get; private set; }

        public DateTime Timestamp { get; private set; }

        public ActivityPoint(Guid activityId, decimal latitude, decimal longitude, DateTime timestamp)
        {
            Id = Guid.NewGuid();
            ActivityId = activityId;
            Latitude = latitude;
            Longitude = longitude;
            Timestamp = timestamp;
        }
    }
}
