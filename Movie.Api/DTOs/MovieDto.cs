using System.Text.Json.Serialization;

namespace Movie.Api.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("original_title")]
        public string OriginalTitle { get; set; } = string.Empty;

        public string Overview { get; set; } = string.Empty;

        [JsonPropertyName("original_language")]
        public string OriginalLanguage { get; set; } = string.Empty;

        [JsonPropertyName("poster_path")]
        public string? PosterPath { get; set; }

        [JsonPropertyName("backdrop_path")]
        public string? BackdropPath { get; set; }

        [JsonPropertyName("release_date")]
        public string? ReleaseDate { get; set; }

        public double Popularity { get; set; }

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }

        [JsonPropertyName("genre_ids")]
        public List<int> GenreIds { get; set; } = new();


    }
}