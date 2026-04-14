using Movie.Api.Models;

namespace Movie.Api.Repositories
{
    public interface IMovieRepository
    {
        Task<Movies?> GetByTmdbIdAsync(int tmdbId);
        Task AddAsync(Movies movie);
    }
}