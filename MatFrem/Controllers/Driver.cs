using MatFrem.Models.DomainModel;
using MatFrem.Models.ViewModel;
using MatFrem.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MatFrem.Controllers
{
	//[Authorize(Roles = "Driver")] //instead of using the [Authorize] attribute on every method, you can use it on the class.
	//Every method need to be authorized to be accessed
	public class Driver : Controller
	{
		private readonly IProductRepository _productRepository;

		private readonly IOrderRepository _orderRepository;
		private readonly UserManager<ApplicationUser> _userManager;

		public Driver(IProductRepository productRepo,
			IOrderRepository orderRepository, UserManager<ApplicationUser> userManager)
		{
			_productRepository = productRepo;
			_orderRepository = orderRepository;
			_userManager = userManager;
		}
		[HttpGet]
		public async Task<IActionResult> DriverPage(int pageSize = 8, int pageNumber = 1)
		{
				var totalRecords = await _productRepository.CountPage();
				var totalPages = (int)Math.Ceiling((decimal)totalRecords / pageSize);

				pageNumber = Math.Clamp(pageNumber, 1, totalPages);

				ViewBag.TotalPages = totalPages;
				ViewBag.CurrentPage = pageNumber;
				ViewBag.PageSize = pageSize;

				var getOrders = await _orderRepository.GetAllOrder(pageNumber, pageSize);

            if (getOrders != null)
            {

				//Here, we just attach our values in the viewmodel, to what is in the database, then send it to the view. 
				//We are in the GET method, so we are just getting the data from the database, and sending it to the view. This happens when the page is loaded.

				var orderViewModels = getOrders.Select(o => new OrderViewModel //need to run through the list and select since its ienumerable in the repository
                {
					OrderID = o.OrderID, //need to pass and attach the ID from DB to the view model, remember that when working with view model
					CustomerName = o.CustomerName ?? string.Empty,
					CustomerPhoneNr = o.CustomerPhoneNr ?? string.Empty,
                    ProductName = o.ProductName ?? string.Empty,
					TotalAmount = o.TotalPrice,
					OrderQuantitySize = o.OrderItem,
					OrderStatusDescription = o.OrderStatus?.StatusDescription ?? string.Empty,
                    PickUpAddress = o.PickUpAddress ?? string.Empty,
                    ItemCategory = o.ProductCategory ?? string.Empty,
					DateOrderCreate = o.OrderCreatedDate,
					DeliveryAddress = o.DeliveryAddress ?? string.Empty
                }).ToList();
                return View(orderViewModels);
            }
            return View();
        }

		[HttpGet]
		public async Task<IActionResult> StartOrder(int id)
		{
			
			var getOrders = await _orderRepository.GetOrderByID(id);

			if(getOrders != null)
			{
				OrderViewModel orderViewModel = new OrderViewModel //we can simply attach to view because GetORderById is not ienumerable (list) in the repository
				{
					OrderID = getOrders.OrderID, //need to pass and attach the ID from DB to the view model, remember that when working with view model
					CustomerName = getOrders.CustomerName ?? string.Empty,
					CustomerPhoneNr = getOrders.CustomerPhoneNr ?? string.Empty,
					ProductName = getOrders.ProductName ?? string.Empty,
					TotalAmount = getOrders.TotalPrice,
					OrderQuantitySize = getOrders.OrderItem,
					OrderStatusDescription = getOrders.OrderStatus?.StatusDescription ?? string.Empty,
					PickUpAddress = getOrders.PickUpAddress ?? string.Empty,
					ItemCategory = getOrders.ProductCategory ?? string.Empty,
					DateOrderCreate = getOrders.OrderCreatedDate,
					DeliveryAddress = getOrders.DeliveryAddress ?? string.Empty
				};

                return View(orderViewModel); //pass the model we have attached to values from the DB, and send it to view
            }

            return NotFound(); //if the order is not found, return not found
        }

		[HttpPost]
		public async Task<IActionResult> StartOrder(int id, int? any)
		{
            var getOrders = await _orderRepository.GetOrderByID(id); //worked now because we attached the id to the view in DriverPage method. 

            var currentUser = await _userManager.GetUserAsync(User); //we can just use this since its only driver than can use this controller/site

            if (getOrders == null)
            {
                return View();
            }

            getOrders.OrderStatusID = 2;
            getOrders.DriverId = currentUser.Id; //we attaching the "currentuser, aka driver" to the order, only driver can use this method anyway
            getOrders.Driver = currentUser;

            await _orderRepository.UpdateOrder(getOrders);
            return RedirectToAction("YourOrder");
        }

		[HttpPost]
		public async Task<IActionResult> FinishOrder(int id)
		{
			var getOrders = await _orderRepository.GetOrderByID(id);
			var currentUser = await _userManager.GetUserAsync(User); //we can just use this since its only driver than can use this controller/site

			if (getOrders == null)
			{
				return View();
			}

			getOrders.OrderStatusID = 3;
            getOrders.DriverId = currentUser.Id;
            getOrders.Driver = currentUser;

            await _orderRepository.UpdateOrder(getOrders);
            return RedirectToAction("YourOrder");
        }



		[HttpGet]
		public async Task<ActionResult> ActiveDeliveries(int id, OrderViewModel orderViewModel)
		{
			var getOrders = await _orderRepository.GetOrderByID(id);
			var getDriver = await _userManager.GetUsersInRoleAsync("Driver"); //we can just use this since its only driver than can use this controller/site

			if (getOrders != null)
			{
				OrderModel orderModel = new OrderModel
				{
                    
                };

				return View(orderModel);


			}
			return View();
		}

		public IActionResult UpdateStatus()
		{
			return View();
		}

		public async Task<IActionResult> DeliveryHistory()
		{
            var getOrders = await _orderRepository.GetAllOrder();
            if (getOrders != null)
            {
                var orderViewModels = getOrders.Select(o => new OrderViewModel
                {
                    CustomerName = o.CustomerName ?? string.Empty,
					DriverId = o.DriverId ?? string.Empty,
                    CustomerPhoneNr = o.CustomerPhoneNr ?? string.Empty,
                    ProductName = o.ProductName ?? string.Empty,
                    TotalAmount = o.TotalPrice,
                    OrderStatusDescription = o.OrderStatus?.StatusDescription ?? string.Empty,
                    PickUpAddress = o.PickUpAddress ?? string.Empty,
                    ItemCategory = o.ProductCategory ?? string.Empty,
                    DateOrderCreate = o.OrderCreatedDate,
                    DeliveryAddress = o.DeliveryAddress ?? string.Empty

                }).ToList();
                return View(orderViewModels);
            }
            return View();
        }

		public IActionResult DeliveryDetails()
		{
			return View();
		}

		public IActionResult ReportLocation()
		{
			return View();
		}
	}



}

