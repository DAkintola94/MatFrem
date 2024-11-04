using Microsoft.AspNetCore.Mvc;

namespace MatFrem.Controllers
{
    public class Location : Controller
    {
        public IActionResult MapGeo()
        {
            return View();
        }
    }
}
