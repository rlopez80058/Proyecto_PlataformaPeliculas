using System.Text.Json.Serialization;

namespace Movie.Api.DTOs
{
    public class TmdbGenreDto
    {
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
    }
}