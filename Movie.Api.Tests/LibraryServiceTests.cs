using Moq;
using Movie.Api.Client;
using Movie.Api.DTOs;
using Movie.Api.Models;
using Movie.Api.Repositories;
using Movie.Api.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Movie.Api.Tests.Services
{
    public class LibraryServiceTests
    {
        private readonly Mock<ILibraryRepository> _libraryRepositoryMock;
        private readonly Mock<IMovieRepository> _movieRepositoryMock;
        private readonly Mock<IActivityRepository> _activityRepositoryMock;
        private readonly Mock<ITmdbClient> _tmdbClientMock;
        private readonly LibraryService _service;

        public LibraryServiceTests()
        {
            _libraryRepositoryMock = new Mock<ILibraryRepository>();
            _movieRepositoryMock = new Mock<IMovieRepository>();
            _activityRepositoryMock = new Mock<IActivityRepository>();
            _tmdbClientMock = new Mock<ITmdbClient>();

            _service = new LibraryService(
                _libraryRepositoryMock.Object,
                _movieRepositoryMock.Object,
                _activityRepositoryMock.Object,
                _tmdbClientMock.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnExistingItem_WhenMovieAlreadyExistsInLibrary()
        {
            var dto = new SaveLibraryItemDto(100, MovieStatus.Pending, null, null);

            var existingMovie = new Movies
            {
                Id = 1,
                TmdbId = 100,
                Title = "Batman",
                OriginalTitle = "Batman",
                Overview = "Test",
                OriginalLanguage = "en"
            };

            var existingItem = new LibraryItem
            {
                Id = 10,
                MovieId = 1,
                Movie = existingMovie,
                Status = MovieStatus.Watched
            };

            _movieRepositoryMock
                .Setup(x => x.GetByTmdbIdAsync(dto.TmdbId))
                .ReturnsAsync(existingMovie);

            _libraryRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<LibraryItem> { existingItem });

            var result = await _service.AddAsync(dto);

            Assert.Equal(existingItem.Id, result.Id);
            _libraryRepositoryMock.Verify(x => x.AddAsync(It.IsAny<LibraryItem>()), Times.Never);
        }

        [Fact]
        public async Task AddAsync_ShouldCreateMovieAndLibraryItem_WhenMovieDoesNotExist()
        {
            var dto = new SaveLibraryItemDto(200, MovieStatus.Pending, 5, "Muy buena");

            _movieRepositoryMock
                .Setup(x => x.GetByTmdbIdAsync(dto.TmdbId))
                .ReturnsAsync((Movies?)null);

            _tmdbClientMock
                .Setup(x => x.GetMovieByIdAsync(dto.TmdbId))
                .ReturnsAsync(new TmdbMovieDetailDto
                {
                    Id = 200,
                    Title = "Interstellar",
                    OriginalTitle = "Interstellar",
                    Overview = "Space movie",
                    OriginalLanguage = "en",
                    ReleaseDate = "2014-11-07",
                    PosterPath = "/poster.jpg",
                    BackdropPath = "/backdrop.jpg",
                    Popularity = 9.5m,
                    VoteAverage = 8.7m
                });

            _movieRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Movies>()))
                .Callback<Movies>(m => m.Id = 2)
                .Returns(Task.CompletedTask);

            _libraryRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<LibraryItem>());

            _libraryRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<LibraryItem>()))
                .Callback<LibraryItem>(item => item.Id = 20)
                .ReturnsAsync((LibraryItem item) => item);

            var result = await _service.AddAsync(dto);

            Assert.Equal(2, result.MovieId);
            Assert.Equal(MovieStatus.Pending, result.Status);
            Assert.Equal(5, result.Rating);
            Assert.Equal("Muy buena", result.Comment);

            _movieRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Movies>()), Times.Once);
            _libraryRepositoryMock.Verify(x => x.AddAsync(It.IsAny<LibraryItem>()), Times.Once);
        }

        [Fact]
        public async Task UpdateStatusAsync_ShouldUpdateStatus_WhenItemExists()
        {
            var item = new LibraryItem
            {
                Id = 1,
                MovieId = 1,
                Movie = new Movies
                {
                    Id = 1,
                    TmdbId = 100,
                    Title = "Interstellar",
                    OriginalTitle = "Interstellar",
                    Overview = "Test",
                    OriginalLanguage = "en"
                },
                Status = MovieStatus.Pending,
                UpdatedAtUtc = DateTime.UtcNow.AddDays(-1)
            };

            _libraryRepositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(item);

            await _service.UpdateStatusAsync(1, new UpdateLibraryStatusDto(MovieStatus.Watched));

            Assert.Equal(MovieStatus.Watched, item.Status);
            _libraryRepositoryMock.Verify(x => x.UpdateAsync(item), Times.Once);
        }

        [Fact]
        public async Task ToggleFavoriteAsync_ShouldInvertIsFavorite_WhenItemExists()
        {
            var item = new LibraryItem
            {
                Id = 1,
                MovieId = 1,
                Movie = new Movies
                {
                    Id = 1,
                    TmdbId = 100,
                    Title = "Batman",
                    OriginalTitle = "Batman",
                    Overview = "Test",
                    OriginalLanguage = "en"
                },
                IsFavorite = false
            };

            _libraryRepositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(item);

            await _service.ToggleFavoriteAsync(1);

            Assert.True(item.IsFavorite);
            _libraryRepositoryMock.Verify(x => x.UpdateAsync(item), Times.Once);
        }
        [Fact]
        public async Task UpdateReviewAsync_ShouldUpdateRatingAndComment_WhenItemExists()
        {
            var item = new LibraryItem
            {
                Id = 1,
                MovieId = 1,
                Movie = new Movies
                {
                    Id = 1,
                    TmdbId = 100,
                    Title = "Inception",
                    OriginalTitle = "Inception",
                    Overview = "Test",
                    OriginalLanguage = "en"
                },
                Rating = null,
                Comment = null
            };

            _libraryRepositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(item);

            await _service.UpdateReviewAsync(1, new UpdateLibraryReviewDto(4, "Buena película"));

            Assert.Equal(4, item.Rating);
            Assert.Equal("Buena película", item.Comment);
            _libraryRepositoryMock.Verify(x => x.UpdateAsync(item), Times.Once);
        }


        [Fact]
        public async Task DeleteAsync_ShouldDeleteItem_WhenItemExists()
        {
            var item = new LibraryItem
            {
                Id = 1,
                MovieId = 1,
                Movie = new Movies
                {
                    Id = 1,
                    TmdbId = 100,
                    Title = "Avatar",
                    OriginalTitle = "Avatar",
                    Overview = "Test",
                    OriginalLanguage = "en"
                }
            };

            _libraryRepositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(item);

            await _service.DeleteAsync(1);

            _libraryRepositoryMock.Verify(x => x.DeleteAsync(item), Times.Once);
        }
    }
}