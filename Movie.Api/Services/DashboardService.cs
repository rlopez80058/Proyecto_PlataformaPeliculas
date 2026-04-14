using Movie.Api.DTOs;
using Movie.Api.Models;
using Movie.Api.Repositories;

namespace Movie.Api.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ILibraryRepository _libraryRepository;
        private readonly IActivityRepository _activityRepository;

        public DashboardService(
            ILibraryRepository libraryRepository,
            IActivityRepository activityRepository)
        {
            _libraryRepository = libraryRepository;
            _activityRepository = activityRepository;
        }

        public async Task<DashboardDto> GetAsync()
        {
            var items = await _libraryRepository.GetAllAsync();
            var activities = await _activityRepository.GetRecentAsync(5);

            return new DashboardDto(
                TotalRecords: items.Count,
                TotalPending: items.Count(x => x.Status == MovieStatus.Pending),
                TotalWatching: items.Count(x => x.Status == MovieStatus.Watching),
                TotalWatched: items.Count(x => x.Status == MovieStatus.Watched),
                MostPopular: new List<PopularMovieDto>(), 
                RecentActivity: activities.Select(a => new ActivityDto(
                    a.ActionType.ToString(),
                    a.Description,
                    a.CreatedAtUtc
                ))
            );
        }
    }
}