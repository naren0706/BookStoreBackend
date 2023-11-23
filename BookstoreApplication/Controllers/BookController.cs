using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
