using Movie.Api.Models;

namespace Movie.Api.Repositories
{
    public interface ILibraryRepository
    {
        Task<LibraryItem> AddAsync(LibraryItem item);
        Task UpdateAsync(LibraryItem item);
        Task<LibraryItem?> GetByIdAsync(int id);
        Task<List<LibraryItem>> GetAllAsync();
        Task<Movies?> GetMovieByTmdbId(int tmdbId);
        Task DeleteAsync(LibraryItem item);

        Task<LibraryItem?> GetByTmdbIdAsync(int tmdbId);
    }
}