using Movie.Api.DTOs;
using Movie.Api.Models;

namespace Movie.Api.Services
{
    public interface ILibraryService
    {
        Task<LibraryItem> AddAsync(SaveLibraryItemDto dto);
        Task<LibraryItem?> GetByTmdbIdAsync(int tmdbId);
        Task<IEnumerable<LibraryItem>> GetAllAsync();
        Task UpdateStatusAsync(int id, UpdateLibraryStatusDto dto);
        Task UpdateReviewAsync(int id, UpdateLibraryReviewDto dto);
        Task ToggleFavoriteAsync(int id);
        Task DeleteAsync(int id);
    }
}