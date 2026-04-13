using Movie.Api.DTOs;

namespace Movie.Api.Client
{
    public interface ITmdbClient
    {
        Task<TmdbSearchResponseDto?> SearchMoviesAsync(string query, int page = 1);
        Task<TmdbSearchResponseDto?> DiscoverMoviesAsync(int? genreId, string? language, int? year, int page = 1);
        Task<TmdbMovieDetailDto?> GetMovieByIdAsync(int tmdbId);
        Task<TmdbSearchResponseDto?> GetRecommendationsAsync(int tmdbId, int page = 1);
        Task<TmdbGenreResponseDto?> GetGenresAsync();
    }
}