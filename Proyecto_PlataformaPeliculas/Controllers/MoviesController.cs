using Microsoft.AspNetCore.Mvc;
using Proyecto_PlataformaPeliculas.Models;
using Proyecto_PlataformaPeliculas.Services;

namespace Proyecto_PlataformaPeliculas.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Recommendations(int id)
        {
            var movie = await _movieService.GetMovieAsync(id);

            if (movie == null)
                return NotFound();

            var recommendations = await _movieService.GetRecommendationsAsync(id);

            var model = new MovieDetailViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                PosterPath = movie.PosterPath,
                Overview = movie.Overview,
                ReleaseDate = movie.ReleaseDate,
                OriginalLanguage = movie.OriginalLanguage,
                VoteAverage = movie.VoteAverage,
                Recommendations = recommendations
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var genres = await _movieService.GetGenresAsync();
            var results = await _movieService.SearchAsync(null, null, null, null);

            var model = new MovieSearchViewModel
            {
                Query = null,
                GenreId = null,
                Language = null,
                Year = null,
                Results = results,
                Genres = genres,
                Languages = GetLanguages(),
                Years = GetYears()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string? query, int? genreId, string? language, int? year)
        {
            var results = await _movieService.SearchAsync(query, genreId, language, year);
            var genres = await _movieService.GetGenresAsync();

            var model = new MovieSearchViewModel
            {
                Query = query,
                GenreId = genreId,
                Language = language,
                Year = year,
                Results = results,
                Genres = genres,
                Languages = GetLanguages(),
                Years = GetYears()
            };

            return View("Index", model);
        }

        private List<int> GetYears()
        {
            var currentYear = DateTime.Now.Year;

            return Enumerable.Range(currentYear - 30, 31)
                             .OrderByDescending(y => y)
                             .ToList();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieAsync(id);

            if (movie == null)
                return NotFound();

            movie.Recommendations = await _movieService.GetRecommendationsAsync(id);

            return View(movie);
        }

        public IActionResult Library()
        {
            return View();
        }

        private List<string> GetLanguages()
        {
            return new List<string>
            {
                "en",
                "es",
                "fr",
                "it",
                "de",
                "pt",
                "ja",
                "ko",
                "zh",
                "ru"
            };
        }
    }
}