using Microsoft.EntityFrameworkCore;
using Movie.Api.Data;
using Movie.Api.Models;

namespace Movie.Api.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly AppDbContext _context;

        public ActivityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ActivityLog log)
        {
            _context.ActivityLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ActivityLog>> GetRecentAsync(int count)
        {
            return await _context.ActivityLogs
                .OrderByDescending(x => x.CreatedAtUtc)
                .Take(count)
                .ToListAsync();
        }
    }
}