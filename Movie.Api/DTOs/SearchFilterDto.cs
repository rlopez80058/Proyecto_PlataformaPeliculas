namespace Movie.Api.DTOs
{
    public record MovieSearchRequestDto(
    string? Query,
    int? GenreId,
    string? Language,
    int? Year,
    int Page = 1
);
}
