using Microsoft.AspNetCore.Mvc;
using Proyecto_PlataformaPeliculas.Models;
using Proyecto_PlataformaPeliculas.Services;

namespace Proyecto_PlataformaPeliculas.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        private readonly IMovieService _movieService;

        public DashboardController(
            IDashboardService dashboardService,
            IMovieService movieService)
        {
            _dashboardService = dashboardService;
            _movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _dashboardService.GetDashboardAsync();

            if (model == null)
            {
                TempData["Error"] = "No se pudo cargar el dashboard.";
                return View(new DashboardViewModel());
            }

            if (model.MostPopular != null && model.MostPopular.Any())
            {
                var baseMovieTmdbId = model.MostPopular.First().TmdbId;
                var recommendations = await _movieService.GetRecommendationsAsync(baseMovieTmdbId);

                model.Recommendations = recommendations?.Take(6).ToList() ?? new List<MovieViewModel>();
            }

            return View(model);
        }
    }
}