using Microsoft.AspNetCore.Mvc;

namespace MatFrem.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
