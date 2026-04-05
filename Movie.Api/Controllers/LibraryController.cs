using Microsoft.AspNetCore.Mvc;

namespace Movie.Api.Controllers
{
    public class LibraryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
