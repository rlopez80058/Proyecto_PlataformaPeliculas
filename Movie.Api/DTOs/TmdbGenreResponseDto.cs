using System.Text.Json.Serialization;

namespace Movie.Api.DTOs
{
    public class TmdbGenreResponseDto
    {
        [JsonPropertyName("genres")]
        public List<TmdbGenreDto> Genres { get; set; } = new();
    }
}