using Movie.Api.DTOs;
using Movie.Api.Models;
using Movie.Api.Repositories;
using Movie.Api.Client;

namespace Movie.Api.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly ILibraryRepository _libraryRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IActivityRepository _activityRepository;
        private readonly ITmdbClient _tmdbClient;

        public LibraryService(
            ILibraryRepository libraryRepository,
            IMovieRepository movieRepository,
            IActivityRepository activityRepository,
            ITmdbClient tmdbClient)
        {
            _libraryRepository = libraryRepository;
            _movieRepository = movieRepository;
            _activityRepository = activityRepository;
            _tmdbClient = tmdbClient;
        }

        public async Task<IEnumerable<LibraryItem>> GetAllAsync()
        {
            return await _libraryRepository.GetAllAsync();
        }

        public async Task<LibraryItem> AddAsync(SaveLibraryItemDto dto)
        {
            var movie = await _movieRepository.GetByTmdbIdAsync(dto.TmdbId);

            if (movie == null)
            {
                var tmdbMovie = await _tmdbClient.GetMovieByIdAsync(dto.TmdbId);

                DateTime parsedDate;

                movie = new Movies
                {
                    TmdbId = dto.TmdbId,
                    Title = tmdbMovie?.Title ?? "Película",
                    OriginalTitle = tmdbMovie?.OriginalTitle ?? "Película",
                    Overview = tmdbMovie?.Overview ?? "Sin descripción",
                    ReleaseDate = tmdbMovie != null && DateTime.TryParse(tmdbMovie.ReleaseDate, out parsedDate)
                        ? parsedDate
                        : DateTime.UtcNow,
                    OriginalLanguage = tmdbMovie?.OriginalLanguage ?? "en",
                    PosterPath = tmdbMovie?.PosterPath ?? "",
                    BackdropPath = tmdbMovie?.BackdropPath ?? "",
                    Popularity = tmdbMovie?.Popularity ?? 0,
                    VoteAverage = tmdbMovie?.VoteAverage ?? 0,
                    CreatedAtUtc = DateTime.UtcNow
                };

                await _movieRepository.AddAsync(movie);
            }

            
            var existing = (await _libraryRepository.GetAllAsync())
                .FirstOrDefault(x => x.MovieId == movie.Id);

            if (existing != null)
                return existing;

            var item = new LibraryItem
            {
                MovieId = movie.Id,
                Status = dto.Status,
                Rating = dto.Rating,
                Comment = dto.Comment,
                IsFavorite = false,
                AddedAtUtc = DateTime.UtcNow,
                UpdatedAtUtc = DateTime.UtcNow
            };

            await _libraryRepository.AddAsync(item);

            return item;
        }

        public async Task UpdateAsync(int id, UpdateLibraryItemDto dto)
        {
            var item = await _libraryRepository.GetByIdAsync(id);
            if (item == null) return;

            item.Status = dto.Status;
            item.Rating = dto.Rating;
            item.Comment = dto.Comment;
            item.UpdatedAtUtc = DateTime.UtcNow;

            await _libraryRepository.UpdateAsync(item);
        }

        public async Task ToggleFavoriteAsync(int id)
        {
            var item = await _libraryRepository.GetByIdAsync(id);
            if (item == null) return;

            item.IsFavorite = !item.IsFavorite;
            item.UpdatedAtUtc = DateTime.UtcNow;

            await _libraryRepository.UpdateAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _libraryRepository.GetByIdAsync(id);
            if (item == null) return;

            await _libraryRepository.DeleteAsync(item);
        }
    }
}