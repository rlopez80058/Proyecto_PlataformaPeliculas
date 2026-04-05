using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Movie.Api.DTOs;
using Movie.Api.Options;

namespace Movie.Api.Client
{
    public class TmdbClient : ITmdbClient
    {
        private readonly HttpClient _http;

        public TmdbClient(HttpClient http, IOptions<TmdbOptions> options)
        {
            _http = http;
            _http.BaseAddress = new Uri(options.Value.BaseUrl);
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", options.Value.ReadAccessToken);
            _http.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<TmdbSearchResponseDto?> SearchMoviesAsync(string query, int page = 1)
        {
            var url = $"search/movie?query={Uri.EscapeDataString(query)}&page={page}";
            return await _http.GetFromJsonAsync<TmdbSearchResponseDto>(url);
        }

        public async Task<TmdbSearchResponseDto?> DiscoverMoviesAsync(int? genreId, string? language, int? year, int page = 1)
        {
            var qs = new List<string> { $"page={page}" };

            if (genreId.HasValue) qs.Add($"with_genres={genreId.Value}");
            if (!string.IsNullOrWhiteSpace(language)) qs.Add($"with_original_language={language}");
            if (year.HasValue) qs.Add($"primary_release_year={year.Value}");

            var url = $"discover/movie?{string.Join("&", qs)}";
            return await _http.GetFromJsonAsync<TmdbSearchResponseDto>(url);
        }

        public async Task<TmdbMovieDetailDto?> GetMovieByIdAsync(int tmdbId)
        {
            return await _http.GetFromJsonAsync<TmdbMovieDetailDto>($"movie/{tmdbId}");
        }
    }
}
