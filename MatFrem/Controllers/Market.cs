using MatFrem.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace MatFrem.Controllers
{
    public class Market : Controller
    {
        private readonly IProductRepository _productRepository;

        public Market(IProductRepository productRepo)
        {
            _productRepository = productRepo;
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
           var deleteItem = await _productRepository.DeleteItem(id);
            return View();
        }
    }
}

