using Movie.Api.DTOs;
using Movie.Api.Models;

namespace Movie.Api.Services
{
    public interface ILibraryService
    {
        Task<LibraryItem> AddAsync(SaveLibraryItemDto dto);
        Task UpdateAsync(int id, UpdateLibraryItemDto dto);
        Task ToggleFavoriteAsync(int id);
        Task<IEnumerable<LibraryItem>> GetAllAsync();
        Task DeleteAsync(int id);
    }
}