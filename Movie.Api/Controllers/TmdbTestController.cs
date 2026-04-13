using Microsoft.AspNetCore.Mvc;
using Movie.Api.Client;

namespace Movie.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TmdbTestController : ControllerBase
    {
        private readonly ITmdbClient _tmdbClient;

        public TmdbTestController(ITmdbClient tmdbClient)
        {
            _tmdbClient = tmdbClient;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] string? query,
            [FromQuery] int? genreId,
            [FromQuery] string? language,
            [FromQuery] int? year,
            [FromQuery] int page = 1)
        {
            if (!string.IsNullOrWhiteSpace(query))
            {
                var result = await _tmdbClient.SearchMoviesAsync(query, page);

                if (result == null)
                    return StatusCode(500, "TMDB no devolvió datos.");

                var filteredResults = result.Results.AsEnumerable();

                if (genreId.HasValue)
                    filteredResults = filteredResults.Where(m => m.Genre_Ids.Contains(genreId.Value));

                if (!string.IsNullOrWhiteSpace(language))
                    filteredResults = filteredResults.Where(m => m.Original_Language == language);

                if (year.HasValue)
                    filteredResults = filteredResults.Where(m =>
                        !string.IsNullOrWhiteSpace(m.Release_Date) &&
                        m.Release_Date.StartsWith(year.Value.ToString()));

                return Ok(filteredResults.ToList());
            }

            var discover = await _tmdbClient.DiscoverMoviesAsync(genreId, language, year, page);

            if (discover == null)
                return StatusCode(500, "TMDB no devolvió datos.");

            return Ok(discover.Results);
        }

        [HttpGet("movie/{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var result = await _tmdbClient.GetMovieByIdAsync(id);

            if (result == null)
                return NotFound("No se encontró la película en TMDB.");

            return Ok(result);
        }

        [HttpGet("movie/{id}/recommendations")]
        public async Task<IActionResult> GetRecommendations(int id, [FromQuery] int page = 1)
        {
            var result = await _tmdbClient.GetRecommendationsAsync(id, page);

            if (result == null)
                return StatusCode(500, "No se pudieron obtener recomendaciones.");

            return Ok(result.Results);
        }
    }
}