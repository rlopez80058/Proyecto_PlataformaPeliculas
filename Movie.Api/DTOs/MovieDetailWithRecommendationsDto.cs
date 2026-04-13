namespace Movie.Api.DTOs
{
    public class MovieDetailWithRecommendationsDto
    {
        public MovieDto? Movie { get; set; }
        public List<MovieDto> Recommendations { get; set; } = new();
    }
}