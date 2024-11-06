using MatFrem.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace MatFrem.Controllers
{
    public class Market : Controller
    {
        private readonly ProductRepository _shoppingRepository;

        public Market(ProductRepository shoppingRepo)
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

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
           var deleteItem = await _shoppingRepository.DeleteItem(id);
            return View();
        }
    }
}

