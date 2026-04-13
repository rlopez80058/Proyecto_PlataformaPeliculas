using Proyecto_PlataformaPeliculas.Models;

namespace Proyecto_PlataformaPeliculas.Services
{
    public interface IMovieService
    {
        Task<List<MovieViewModel>> SearchAsync(string? query, int? genreId, string? language, int? year);
        Task<MovieDetailViewModel?> GetMovieAsync(int id);
        Task<List<MovieViewModel>> GetRecommendationsAsync(int id);
        Task<List<GenreViewModel>> GetGenresAsync();
    }
}