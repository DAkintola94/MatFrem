using MatFrem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MatFrem.Controllers
{
    public class Driver : Controller
    {
        private readonly IProductRepository _productRepository;

        public Driver(IProductRepository productRepo)
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
