using Microsoft.AspNetCore.Mvc;

namespace AbaJohn.Controllers
{
    public class newController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
