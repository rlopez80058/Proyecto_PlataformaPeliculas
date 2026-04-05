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
        public async Task<IActionResult> Search([FromQuery] string query = "Inception")
        {
            var result = await _tmdbClient.SearchMoviesAsync(query);

            if (result == null)
                return StatusCode(500, "TMDB no devolvió datos.");

            return Ok(result);
        }

        [HttpGet("movie/{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var result = await _tmdbClient.GetMovieByIdAsync(id);

            if (result == null)
                return NotFound("No se encontró la película en TMDB.");

            return Ok(result);
        }
    }
}
