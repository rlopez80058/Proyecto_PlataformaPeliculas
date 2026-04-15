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

        private string GetFriendlyActionType(ActivityType actionType)
        {
            return actionType switch
            {
                ActivityType.AddedToLibrary => "Película agregada a biblioteca",
                ActivityType.StatusChanged => "Estado de película actualizado",
                ActivityType.Rated => "Película calificada",
                ActivityType.Commented => "Comentario agregado o actualizado",
                ActivityType.FavoriteToggled => "Película agregada a favoritos",
                ActivityType.RemovedFromLibrary => "Película eliminada de biblioteca",
                _ => "Actividad de biblioteca"
            };
        }

        public async Task<DashboardDto> GetAsync()
        {
            var items = await _libraryRepository.GetAllAsync();
            var activities = await _activityRepository.GetRecentAsync(5);

            var mostPopular = items
                .Where(x => x.Movie != null)
                .GroupBy(x => new
                {
                    x.MovieId,
                    x.Movie.TmdbId,
                    x.Movie.Title,
                    x.Movie.PosterPath
                })
                .Select(g => new PopularMovieDto(
                    MovieId: g.Key.MovieId,
                    TmdbId: g.Key.TmdbId,
                    Title: g.Key.Title,
                    PosterUrl: g.Key.PosterPath,
                    TimesAdded: g.Count()
                ))
                .OrderByDescending(x => x.TimesAdded)
                .ThenBy(x => x.Title)
                .Take(5)
                .ToList();

            return new DashboardDto(
                TotalRecords: items.Count,
                TotalPending: items.Count(x => x.Status == MovieStatus.Pending),
                TotalWatching: items.Count(x => x.Status == MovieStatus.Watching),
                TotalWatched: items.Count(x => x.Status == MovieStatus.Watched),
                MostPopular: mostPopular,
               RecentActivity: activities.Select(a => new ActivityDto(
    GetFriendlyActionType(a.ActionType),
    a.Description,
    a.CreatedAtUtc
))
            );
        }
    }
}