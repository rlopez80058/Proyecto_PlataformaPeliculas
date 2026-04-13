using System.Text.Json.Serialization;

namespace Proyecto_PlataformaPeliculas.Models
{
    public class MovieDetailViewModel
    {

        [JsonPropertyName("genres")]
        public List<GenreViewModel> Genres { get; set; } = new();

        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Overview { get; set; } = string.Empty;

        [JsonPropertyName("poster_path")]
        public string? PosterPath { get; set; }

        [JsonPropertyName("backdrop_path")]
        public string? BackdropPath { get; set; }

        [JsonPropertyName("release_date")]
        public string? ReleaseDate { get; set; }

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }

        [JsonPropertyName("original_language")]
        public string? OriginalLanguage { get; set; }

        [JsonPropertyName("original_title")]
        public string? OriginalTitle { get; set; }

        public List<MovieViewModel> Recommendations { get; set; } = new();
    }
}