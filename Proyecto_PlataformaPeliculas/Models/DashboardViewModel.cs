namespace Proyecto_PlataformaPeliculas.Models
{
    public class DashboardViewModel
    {
        public int TotalRecords { get; set; }
        public int TotalPending { get; set; }
        public int TotalWatching { get; set; }
        public int TotalWatched { get; set; }

        public List<PopularMovieViewModel> MostPopular { get; set; } = new();
        public List<ActivityViewModel> RecentActivity { get; set; } = new();

        public List<MovieViewModel> Recommendations { get; set; } = new();
    }

    public class PopularMovieViewModel
    {
        public int MovieId { get; set; }
        public int TmdbId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? PosterUrl { get; set; }
        public int TimesAdded { get; set; }
    }

    public class ActivityViewModel
    {
        public string ActionType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAtUtc { get; set; }
    }
}