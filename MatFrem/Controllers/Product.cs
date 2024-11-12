using MatFrem.Models.DomainModel;
using MatFrem.Models.ViewModel;
using MatFrem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MatFrem.Controllers
{
    public class Product : Controller
    {
        private readonly IProductRepository _productRepository;

        public Product(IProductRepository productRepo)
        {
            _productRepository = productRepo;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(ProductModel pModel) //no need for a get method, you have directed the html form in Index here
        {


            if (ModelState.IsValid)
            {
                var addItems = await _productRepository.InsertProduct(pModel); //using Insert because Save does not take a parameter
			}
            return RedirectToAction("ShowProduct");
        }

        [HttpGet]
        public async Task<ActionResult> ShowProduct(int pageSize = 8, int pageNumber = 1)
        {
            var totalRecords = await _productRepository.CountPage();
            var totalPages = (int)Math.Ceiling((decimal)totalRecords / pageSize);

            pageNumber = Math.Clamp(pageNumber, 1, totalPages);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.PageSize = pageSize;

            var showAll = await _productRepository.GetAllItems(pageNumber, pageSize);
            return View(showAll); //need to return a list, its IEnumerable in the html
        }

        [HttpGet]
        public async Task<ActionResult> EditProduct(int id)
        {
            var editItem = await _productRepository.GetItemById(id); //this repository have not saved anything, only found the id
            if (editItem != null)
            {
                EditProductModel editProduct = new EditProductModel
                {
                    ProductID = editItem.ProductID,
                    ProductName = editItem.ProductName,
                    ProductPrice = editItem.ProductPrice,
                    ProductCalories = editItem.ProductCalories,
                    ProductCategory = editItem.ProductCategory
                };
                return View(editProduct); //its this "new" model we want to return, editItem is attached to another type of model that is not seeded here
            }
            return View(null);
        }

        [HttpPost]
        public async Task<ActionResult> EditProduct(EditProductModel editProduct)
        {
            // Retrieve the existing product from the database by ID, from table Product_detail.
            var existingItem = await _productRepository.GetItemById(editProduct.ProductID);

            if (existingItem != null) // Check if the item exists in the database
            {
                // Update the properties of the existing item
                existingItem.ProductName = editProduct.ProductName;
                existingItem.ProductPrice = editProduct.ProductPrice;
                existingItem.ProductCalories = editProduct.ProductCalories;
                existingItem.ProductCategory = editProduct.ProductCategory;

                // Save the changes to the repository
                await _productRepository.UpdateItems(existingItem);

                return RedirectToAction("Edit", new { id = editProduct.ProductID });
            }
            
            
                return NotFound();
            

        }

        [HttpPost]
        public async Task<ActionResult> DeleteProduct(EditProductModel editProduct)
        {
            var findItem = await _productRepository.GetItemById(editProduct.ProductID);

            if(findItem != null)
            {
                var deleteItem = await _productRepository.DeleteItem(editProduct.ProductID); //this deletes the row since ProductID is the primary key!!
                return RedirectToAction("ShowProduct");
            }

            return NotFound();
        }
    }
}
