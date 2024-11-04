using Microsoft.AspNetCore.Mvc;

namespace MatFrem.Controllers
{
    public class Driver : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
