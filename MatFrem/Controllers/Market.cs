using MatFrem.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace MatFrem.Controllers
{
    public class Market : Controller
    {
        private readonly ShoppingRepository _shoppingRepository;

        public Market(ShoppingRepository shoppingRepo)
        {
            _shoppingRepository = shoppingRepo;
        }
        public IActionResult Shopping()
        {
            return View();
        }

        public IActionResult Cart()
        {
            return View();
        }
    }
}

