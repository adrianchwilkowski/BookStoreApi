using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class BookStoreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
