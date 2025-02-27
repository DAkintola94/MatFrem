using MatFrem.Models.DomainModel;
using MatFrem.Models.ViewModel;
using MatFrem.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MatFrem.Controllers
{
	//[Authorize(Roles = "System Administrator")] //instead of using the [Authorize] attribute on every method, you can use it on the class.
                                                //Every method need to be authorized to be accessed
	public class Product : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnviroment;
		private readonly string wwwRootPath;

		public Product(IProductRepository productRepo, IWebHostEnvironment webHostEnviroment)
		{
			_productRepository = productRepo;
			_webHostEnviroment = webHostEnviroment;
			wwwRootPath = _webHostEnviroment.WebRootPath;
		}

        [HttpGet]
        public async Task<IActionResult> Index()
        {
			return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ProductViewModel pModel, IFormFile? file) //file need to have a name ="" in the html, so it can be passed as a parameter									 // when you are not using model in the controller parameter, you need to use the name of the input field in the html
		{
			if (ModelState.IsValid)
			{

				ProductModel productModel = new ProductModel
				{

					ProductID = pModel.ProductID,
					ProductName = pModel.ProductViewName,
					ProductPrice = pModel.ProductViewPrice,
					ProductCalories = pModel.ProductViewCalories,
					ProductLocation = pModel.ProductViewLocation,
					CategoryId = pModel.ViewMCategoryId,
                    ShopId = pModel.ViewMShopId //getting the value from the option selector in html, then inserting it into the db and to the designed foreign key
                };

                    if (file != null)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string productPath = Path.Combine(wwwRootPath, "Images", "product");

      

                        using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                    productModel.ImageUrl = Path.Combine("Images", "product", fileName);

                }

                    await _productRepository.InsertProduct(productModel);
					return RedirectToAction("ShowProduct");
			}
             return View(pModel);
        }

		
		[HttpGet]
        public async Task<IActionResult> ShowProduct(int pageSize = 8, int pageNumber = 1)
        {
            var totalRecords = await _productRepository.CountPage();
            var totalPages = (int)Math.Ceiling((decimal)totalRecords / pageSize);

            totalPages = Math.Max(totalPages, 1);

            pageNumber = Math.Clamp(pageNumber, 1, totalPages);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.PageSize = pageSize;

            var getAllProducts = await _productRepository.GetAllItems(pageNumber, pageSize);

            if (getAllProducts != null)
            {
                var productViewModels = getAllProducts.Select(product => new ProductViewModel
                {
                    ProductID = product.ProductID,
                    ProductViewName = product.ProductName,
                    ProductViewPrice = product.ProductPrice,
                    ProductViewCalories = product.ProductCalories,
                    ProductViewLocation = product.ProductLocation,
                    ViewCategoryName = product.CategoryModel.CategoryName ?? "Null",
                    ViewShopName = product.ShopModelO.ShopName ?? "Null", //getting the value from the shop model property, which semi act as a foreign key
                    ViewMShopId = product.ShopId,
                    ImageUrl = product.ImageUrl
                }).ToList();
                return View(productViewModels);
            }
			return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var editItem = await _productRepository.GetItemById(id); //this repository have not saved anything, only found the id
            if (editItem != null)
            {
                ProductViewModel editProduct = new ProductViewModel
                {
                    ProductID = editItem.ProductID,
                    ProductViewName = editItem.ProductName,
                    ProductViewPrice = editItem.ProductPrice,
                    ProductViewCalories = editItem.ProductCalories,
                    ProductViewLocation = editItem.ProductLocation,
					ViewCategoryName = editItem.CategoryModel.CategoryName,
                    ViewShopName = editItem.ShopModelO.ShopName,
                    ViewMCategoryId = editItem.CategoryId
				};
				return View(editProduct); //its this "new" model we want to return, editItem is attached to another type of model that is not seeded here
            }
            return View(null);
        }


		[HttpPost]
		public async Task<IActionResult> EditProduct(ProductViewModel editProduct, IFormFile? file, int id)
		{
			var existingItem = await _productRepository.GetItemById(id);
			if (existingItem == null)
			{
				return NotFound();
			}

			if (file != null)
			{
				string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
				string productPath = Path.Combine(wwwRootPath, "Images", "product");

				// Delete the old image if it exists
				var oldImagePath = Path.Combine(_webHostEnviroment.WebRootPath, existingItem.ImageUrl.TrimStart('\\'));
				if (System.IO.File.Exists(oldImagePath))
				{
					System.IO.File.Delete(oldImagePath);
				}

				// Save the new image
				using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
				{
					await file.CopyToAsync(fileStream);
				}
				existingItem.ImageUrl = Path.Combine("Images", "product", fileName);
			}

			// Update the existing item with new values
			existingItem.ProductName = editProduct.ProductViewName;
			existingItem.ProductPrice = editProduct.ProductViewPrice;
			existingItem.ProductCalories = editProduct.ProductViewCalories;
			existingItem.ProductLocation = editProduct.ProductViewLocation;
			existingItem.Description = editProduct.ProductViewDescription;
			existingItem.CategoryId = editProduct.ViewMCategoryId;
            existingItem.ShopId = editProduct.ViewMShopId;

			// Save the changes to the repository
			await _productRepository.UpdateItems(existingItem);

			return RedirectToAction("ShowProduct", new { id = editProduct.ProductID });
		}
        
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var findItem = await _productRepository.GetItemById(id);
            if (findItem == null)
            {
                return NotFound();
            }

            var oldImagePath = Path.Combine(_webHostEnviroment.WebRootPath, findItem.ImageUrl?.TrimStart('\\') ?? string.Empty);

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            await _productRepository.DeleteItem(id);
            return RedirectToAction("ShowProduct");
        }


        [HttpGet]
		public async Task<ActionResult> Detail(int id)
		{
			var getById = await _productRepository.GetItemById(id);
            if (getById != null)
            {
				ProductViewModel viewModel = new ProductViewModel
				{
                    ProductViewPrice = getById.ProductPrice,
                    ProductViewName = getById.ProductName,
					ProductViewCalories = getById.ProductCalories,
					ProductViewLocation = getById.ProductLocation,
					ProductViewDescription = getById.Description
				};

                return View(viewModel);
            }
			return View();
        }

    }
}
