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
        public async Task<ActionResult> Index()
        {
            var getAllProducts = await _productRepository.GetAllItems();
            if (getAllProducts != null)
            {
                var productViewModels = getAllProducts.Select(product => new ProductViewModel
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    ProductCalories = product.ProductCalories,
                    ProductLocation = product.ProductLocation,
                    Category = product.Category,
                    ImageUrl = product.ImageUrl
                }).ToList();

                return View(productViewModels);
            }

            return View(new List<ProductViewModel>());
        }



        [HttpPost]
        public async Task<ActionResult> Index(ProductViewModel pModel, IFormFile? file, int? id) //file need to have a name ="" in the html, so it can be passed as a parameter
																						 // when you are not using model in the controller parameter, you need to use the name of the input field in the html
		{
			if (id != null && id != 0)
			{
				var getModelId = await _productRepository.GetItemById(id.Value);
				if (getModelId != null)
				{
					ProductViewModel editProductModel = new ProductViewModel
					{
						ProductID = getModelId.ProductID,
						ProductName = getModelId.ProductName,
						ProductPrice = getModelId.ProductPrice,
						ProductCalories = getModelId.ProductCalories,
						ProductLocation = getModelId.ProductLocation,
						Category = getModelId.Category,
						ImageUrl = getModelId.ImageUrl
					};

                    var updateProduct = await _productRepository.UpdateItems(getModelId);
					return RedirectToAction("ShowProduct");
				}
			}


			if (ModelState.IsValid)
			{

                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath + @"Images\product");
					
					if(!string.IsNullOrEmpty(pModel.ImageUrl))
					{
						//delete the old image
						var oldImagePath = Path.Combine(wwwRootPath, pModel.ImageUrl.TrimStart('\\'));
						if (System.IO.File.Exists(oldImagePath))
						{
							System.IO.File.Delete(oldImagePath);
						}


					}

					using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
					{
						await file.CopyToAsync(fileStream);
					}

					pModel.ImageUrl = @"Images\product" + fileName;
				}

				ProductModel productView = new ProductModel
			    {
                    ProductName = pModel.ProductName,
				    ProductPrice = pModel.ProductPrice,
				    ProductCalories = pModel.ProductCalories,
				    ProductLocation = pModel.ProductLocation,
					Category = pModel.Category,
					ImageUrl = pModel.ImageUrl
				}; //so we can save the productmodel to the database, but formatted into the viewmodel

				if(pModel.ProductID == null)
				{
					var addItems = await _productRepository.InsertProduct(productView);
					return RedirectToAction("ShowProduct");
				}

				var updateItems = await _productRepository.UpdateItems(productView);
				return RedirectToAction("ShowProduct");
			}


             return View(pModel);
			
        }

		
		[HttpGet]
        public async Task<ActionResult> ShowProduct(int pageSize = 8, int pageNumber = 1)
        {
            var totalRecords = await _productRepository.CountPage();
            var totalPages = (int)Math.Ceiling((decimal)totalRecords / pageSize);

            totalPages = Math.Max(totalPages, 1);

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
                ProductViewModel editProduct = new ProductViewModel
                {
                    ProductID = editItem.ProductID,
                    ProductName = editItem.ProductName,
                    ProductPrice = editItem.ProductPrice,
                    ProductCalories = editItem.ProductCalories,
                    ProductLocation = editItem.ProductLocation,
					Category = editItem.Category

				};
				return View(editProduct); //its this "new" model we want to return, editItem is attached to another type of model that is not seeded here
            }
            return View(null);
        }


		[HttpPost]
		public async Task<ActionResult> EditProduct(ProductViewModel editProduct, IFormFile? file)
		{
			var existingItem = await _productRepository.GetItemById(editProduct.ProductID);
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
			existingItem.ProductName = editProduct.ProductName;
			existingItem.ProductPrice = editProduct.ProductPrice;
			existingItem.ProductCalories = editProduct.ProductCalories;
			existingItem.ProductLocation = editProduct.ProductLocation;
			existingItem.Description = editProduct.Description;
			existingItem.Category = editProduct.Category;

			// Save the changes to the repository
			await _productRepository.UpdateItems(existingItem);

			return RedirectToAction("ShowProduct", new { id = editProduct.ProductID });
		}


		[HttpDelete]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var findItem = await _productRepository.GetItemById(id);
			var oldImagePath =
				Path.Combine(_webHostEnviroment.WebRootPath, findItem.ImageUrl.TrimStart('\\'));

			if (findItem != null)
            {
				if (System.IO.File.Exists(oldImagePath))
				{
					System.IO.File.Delete(oldImagePath);
				}
				var deleteItem = await _productRepository.DeleteItem(id); //this deletes the row since ProductID is the primary key!!
				return Json(new { success = true, message = "Delete successful" });
				
            }

			return Json(new { success = false, message = "Delete failed" });
		}
    }
}
