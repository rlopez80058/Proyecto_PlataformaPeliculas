using Microsoft.AspNetCore.Mvc;

namespace Movie.Api.Controllers
{
    public class GenresController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
