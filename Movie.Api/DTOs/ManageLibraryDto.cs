using Movie.Api.Models;

namespace Movie.Api.DTOs
{
    public record SaveLibraryItemDto(
    int TmdbId,
    MovieStatus Status,
    int? Rating,
    string? Comment
);

    public record UpdateLibraryItemDto(
        MovieStatus Status,
        int? Rating,
        string? Comment
    );

    public record LibraryItemDto(
        int Id,
        int MovieId,
        int TmdbId,
        string Title,
        string? PosterUrl,
        string Status,
        int? Rating,
        string? Comment,
        DateTime AddedAtUtc,
        DateTime UpdatedAtUtc
    );

    public record DashboardDto(
        int TotalRecords,
        int TotalPending,
        int TotalWatching,
        int TotalWatched,
        IEnumerable<PopularMovieDto> MostPopular,
        IEnumerable<ActivityDto> RecentActivity
    );

    public record PopularMovieDto(
        int MovieId,
        int TmdbId,
        string Title,
        string? PosterUrl,
        int TimesAdded
    );

    public record ActivityDto(
        string ActionType,
        string Description,
        DateTime CreatedAtUtc
    );
}
