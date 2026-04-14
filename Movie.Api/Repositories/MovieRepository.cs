using Microsoft.EntityFrameworkCore;
using Movie.Api.Data;
using Movie.Api.Models;

namespace Movie.Api.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Movies?> GetByTmdbIdAsync(int tmdbId)
        {
            return await _context.Movies
                .FirstOrDefaultAsync(x => x.TmdbId == tmdbId);
        }

        public async Task AddAsync(Movies movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }
    }
}