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

        [HttpGet]
        public async Task<IActionResult> Shopping()
        {
            var listAllProducts = await _productRepository.GetAllItems();
            return View(listAllProducts);
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

