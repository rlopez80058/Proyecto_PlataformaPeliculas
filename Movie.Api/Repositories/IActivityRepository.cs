using Movie.Api.Models;

namespace Movie.Api.Repositories
{
    public interface IActivityRepository
    {
        Task AddAsync(ActivityLog log);
        Task<List<ActivityLog>> GetRecentAsync(int count);
    }
}