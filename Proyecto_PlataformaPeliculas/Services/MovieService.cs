using System.Net.Http.Json;
using Proyecto_PlataformaPeliculas.Models;

namespace Proyecto_PlataformaPeliculas.Services
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _httpClient;

        public MovieService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MovieViewModel>> SearchAsync(string? query, int? genreId, string? language, int? year)
        {
            var url = "api/TmdbTest/search?";
            var parameters = new List<string>();

            if (!string.IsNullOrWhiteSpace(query))
                parameters.Add($"query={Uri.EscapeDataString(query)}");

            if (genreId.HasValue)
                parameters.Add($"genreId={genreId.Value}");

            if (!string.IsNullOrWhiteSpace(language))
                parameters.Add($"language={Uri.EscapeDataString(language)}");

            if (year.HasValue)
                parameters.Add($"year={year.Value}");

            url += string.Join("&", parameters);

            var result = await _httpClient.GetFromJsonAsync<List<MovieViewModel>>(url);
            return result ?? new List<MovieViewModel>();
        }

        public async Task<MovieDetailViewModel?> GetMovieAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<MovieDetailViewModel>($"api/TmdbTest/movie/{id}");
        }

        public async Task<List<MovieViewModel>> GetRecommendationsAsync(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<List<MovieViewModel>>($"api/TmdbTest/movie/{id}/recommendations");
            return result ?? new List<MovieViewModel>();
        }

        public async Task<List<GenreViewModel>> GetGenresAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<GenreViewModel>>("api/Genres");
            return result ?? new List<GenreViewModel>();
        }
    }
}