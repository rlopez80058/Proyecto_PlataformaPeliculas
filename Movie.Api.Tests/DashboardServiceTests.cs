using Moq;
using Movie.Api.Models;
using Movie.Api.Repositories;
using Movie.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Movie.Api.Tests.Services
{
    public class DashboardServiceTests
    {
        private readonly Mock<ILibraryRepository> _libraryRepositoryMock;
        private readonly Mock<IActivityRepository> _activityRepositoryMock;
        private readonly DashboardService _service;

        public DashboardServiceTests()
        {
            _libraryRepositoryMock = new Mock<ILibraryRepository>();
            _activityRepositoryMock = new Mock<IActivityRepository>();

            _service = new DashboardService(
                _libraryRepositoryMock.Object,
                _activityRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnCorrectCountsAndRecentActivity()
        {
            var movie1 = new Movies
            {
                Id = 1,
                TmdbId = 100,
                Title = "Movie 1",
                OriginalTitle = "Movie 1",
                Overview = "Test",
                OriginalLanguage = "en",
                PosterPath = "/m1.jpg"
            };

            var movie2 = new Movies
            {
                Id = 2,
                TmdbId = 200,
                Title = "Movie 2",
                OriginalTitle = "Movie 2",
                Overview = "Test",
                OriginalLanguage = "en",
                PosterPath = "/m2.jpg"
            };

            var items = new List<LibraryItem>
            {
                new LibraryItem { Id = 1, MovieId = 1, Movie = movie1, Status = MovieStatus.Pending },
                new LibraryItem { Id = 2, MovieId = 2, Movie = movie2, Status = MovieStatus.Watching },
                new LibraryItem { Id = 3, MovieId = 1, Movie = movie1, Status = MovieStatus.Watched }
            };

            var activities = new List<ActivityLog>
            {
                new ActivityLog
                {
                    Id = 1,
                    ActionType = ActivityType.AddedToLibrary,
                    Description = "Se agregó Movie 1",
                    CreatedAtUtc = DateTime.UtcNow
                },
                new ActivityLog
                {
                    Id = 2,
                    ActionType = ActivityType.StatusChanged,
                    Description = "Cambio de estado",
                    CreatedAtUtc = DateTime.UtcNow
                }
            };

            _libraryRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(items);

            _activityRepositoryMock
                .Setup(x => x.GetRecentAsync(5))
                .ReturnsAsync(activities);

            var result = await _service.GetAsync();

            Assert.Equal(3, result.TotalRecords);
            Assert.Equal(1, result.TotalPending);
            Assert.Equal(1, result.TotalWatching);
            Assert.Equal(1, result.TotalWatched);
            Assert.Equal(2, result.RecentActivity.Count());
        }
    }
}