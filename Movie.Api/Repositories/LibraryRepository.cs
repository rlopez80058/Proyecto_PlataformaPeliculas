using Microsoft.EntityFrameworkCore;
using Movie.Api.Data;
using Movie.Api.Models;

namespace Movie.Api.Repositories
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly AppDbContext _context;

        public LibraryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LibraryItem> AddAsync(LibraryItem item)
        {
            _context.LibraryItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task UpdateAsync(LibraryItem item)
        {
            _context.LibraryItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task<LibraryItem?> GetByIdAsync(int id)
        {
            return await _context.LibraryItems
                .Include(x => x.Movie)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Movies?> GetMovieByTmdbId(int tmdbId)
        {
            return await _context.Movies
                .FirstOrDefaultAsync(m => m.TmdbId == tmdbId);
        }

        public async Task<List<LibraryItem>> GetAllAsync()
        {
            return await _context.LibraryItems
                .Include(x => x.Movie)
                .ToListAsync();
        }

        public async Task DeleteAsync(LibraryItem item)
        {
            _context.Entry(item).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}