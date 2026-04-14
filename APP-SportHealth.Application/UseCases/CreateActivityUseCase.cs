using APP_SportHealth.Application.Interfaces;
using APP_SportHealth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APP_SportHealth.Application.UseCases
{
    public class CreateActivityUseCase
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IUserRepository _userRepository;

        public CreateActivityUseCase(IActivityRepository activityRepository, IUserRepository userRepository)
        {
            _activityRepository = activityRepository;
            _userRepository = userRepository;
        }

        public async Task Execute(Guid userId, decimal distance, int duration, List<(decimal latitude, decimal longitude, DateTime timestamp)>? points)
        {
            if (distance <= 0) throw new ArgumentException("Distance must be > 0", nameof(distance));
            if (duration <= 0) throw new ArgumentException("Duration must be > 0", nameof(duration));

            var activity = new Activity(userId, distance, duration);

            // compute avg pace (seconds per km)
            if (distance > 0)
            {
                var avgPace = (decimal)duration / distance; // seconds per km
                var prop = activity.GetType().GetProperty("AvgPace");
                prop?.SetValue(activity, avgPace);
            }

            // simple calories estimate: assume weight 70kg and MET 9
            var calories = 70m * 9m * ((decimal)duration / 3600m);
            var propCal = activity.GetType().GetProperty("Calories");
            propCal?.SetValue(activity, calories);

            var propEnded = activity.GetType().GetProperty("EndedAt");
            propEnded?.SetValue(activity, DateTime.UtcNow);

            // validate user exists
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) throw new ArgumentException("User not found", nameof(userId));

            // validate points before persisting to avoid DB errors (lat: -90..90, lon: -180..180)
            if (points != null && points.Count > 0)
            {
                foreach (var p in points)
                {
                    if (p.latitude < -90m || p.latitude > 90m)
                        throw new APP_SportHealth.Application.Exceptions.BusinessException($"Latitude value {p.latitude} is out of range (-90..90)");

                    if (p.longitude < -180m || p.longitude > 180m)
                        throw new APP_SportHealth.Application.Exceptions.BusinessException($"Longitude value {p.longitude} is out of range (-180..180)");

                    var point = new ActivityPoint(activity.Id, p.latitude, p.longitude, p.timestamp);
                    activity.Points.Add(point);
                }
            }

            await _activityRepository.Create(activity);
        }
    }
}
