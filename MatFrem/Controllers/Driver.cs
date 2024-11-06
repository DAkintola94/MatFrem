using MatFrem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MatFrem.Controllers
{
    public class Driver : Controller
    {
        private readonly ProductRepository _productRepository;

        public Driver(ProductRepository productRepo)
        {
            _productRepository = productRepo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Delete(int id)
        {
            var deleteItem = await _productRepository.DeleteItem(id);
            return View();
        }

    }
}
