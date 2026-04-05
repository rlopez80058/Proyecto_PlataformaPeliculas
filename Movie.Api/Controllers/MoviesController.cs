using Microsoft.AspNetCore.Mvc;

namespace Movie.Api.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
