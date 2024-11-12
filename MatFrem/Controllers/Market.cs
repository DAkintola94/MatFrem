using MatFrem.Models.DomainModel;
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
        public async Task<IActionResult> Shopping(int pageSize = 8, int pageNumber = 1)
        {
            
            var totalRecords = await _productRepository.CountPage();
            var totalPages = (int)Math.Ceiling((decimal)totalRecords / pageSize);

            pageNumber = Math.Clamp(pageNumber, 1, totalPages);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.PageSize = pageSize;

            var listAllProducts = await _productRepository.GetAllItems(pageNumber, pageSize);

            return View(listAllProducts);
        }

        [HttpPost]
        public async Task<ActionResult> Cart(int id)
        {

            var getById = await _productRepository.GetItemById(id);
            if(getById != null)
            {
                var editProductView = new ProductModel
                {
                    ProductID = getById.ProductID,
                    ProductName = getById.ProductName,
                    ProductPrice = getById.ProductPrice,
                    ProductCalories = getById.ProductCalories,
                };

            }
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

