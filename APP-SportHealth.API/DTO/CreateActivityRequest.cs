using System;
using System.Collections.Generic;

namespace APP_SportHealth.API.DTO
{
    public class ActivityPointDto
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class CreateActivityRequest
    {
        public Guid UserId { get; set; }
        public decimal Distance { get; set; }
        public int Duration { get; set; }
        public List<ActivityPointDto>? Points { get; set; }
    }
}
