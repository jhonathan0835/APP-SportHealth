using APP_SportHealth.Application.Interfaces;
using APP_SportHealth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace APP_SportHealth.Infrastructure
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly AppDbContext _context;

        public ActivityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Activity activity)
        {
            _context.Activities.Add(activity);
            if (activity.Points != null && activity.Points.Count > 0)
            {
                _context.ActivityPoints.AddRange(activity.Points);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Activity?> GetById(Guid id)
        {
            return await _context.Activities
                .Include(a => a.Points)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Activity>> ListByUser(Guid userId)
        {
            return await _context.Activities
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.StartedAt)
                .ToListAsync();
        }
    }
}
