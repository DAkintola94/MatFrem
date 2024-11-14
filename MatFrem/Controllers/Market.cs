using MatFrem.Models.DomainModel;
using MatFrem.Models.ViewModel;
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

        [HttpGet]
        public async Task<ActionResult> Cart(int id)
        {
            var listById = await _productRepository.GetItemById(id); //We are getting data from database by its id (primary key)
			if (listById != null)
            {
                var editProductView = new EditProductModel //this way, we attach the data model from db into the view model
				{
                    ProductID = listById.ProductID, //by doing this
                    ProductName = listById.ProductName,
                    ProductPrice = listById.ProductPrice,
                    ProductCalories = listById.ProductCalories,
                    ProductLocation = listById.ProductLocation
                };

				return View(editProductView); //we are returning the view model to the view
			}
            return View();
        }

        [HttpPost]
		public IActionResult Cart()
		{
			return View();
		}

        public async Task<ActionResult> Buy(int id)
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

