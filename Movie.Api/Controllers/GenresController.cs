using Microsoft.AspNetCore.Mvc;
using Movie.Api.Client;

namespace Movie.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly ITmdbClient _tmdbClient;

        public GenresController(ITmdbClient tmdbClient)
        {
            _tmdbClient = tmdbClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var result = await _tmdbClient.GetGenresAsync();

            if (result == null || result.Genres == null)
                return StatusCode(500, "No se pudieron obtener los géneros.");

            return Ok(result.Genres);
        }
    }
}