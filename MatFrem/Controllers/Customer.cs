using Microsoft.AspNetCore.Mvc;

namespace MatFrem.Controllers
{
    public class Customer : Controller
    {
        public IActionResult CreateAccount()
        {
            return View();
        }

        public IActionResult Orders()
        {
            return View();
        }


    }
}
