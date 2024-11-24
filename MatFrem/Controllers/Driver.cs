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
		private readonly ILocationRepository _locationRepository;
		private readonly IOrderRepository _orderRepository;
		private readonly UserManager<ApplicationUser> _userManager;

		public Driver(IProductRepository productRepo, ILocationRepository locationRepo,
			IOrderRepository orderRepository, UserManager<ApplicationUser> userManager)
		{
			_productRepository = productRepo;
			_locationRepository = locationRepo;
			_orderRepository = orderRepository;
			_userManager = userManager;
		}
		[HttpGet]
		public IActionResult DriverPage()
		{
			return View();
		}

		public IActionResult CheckOrders()
		{
			return View();
		}
		[HttpGet]
		public async Task<ActionResult> ActiveDeliveries(int id, OrderViewModel orderViewModel)
		{
			var getOrders = await _orderRepository.GetOrderByID(id);
			var getDriver = await _userManager.GetUsersInRoleAsync("Driver"); //getusersinroleasync gets all users in a role.

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

		public IActionResult DeliveryHistory()
		{
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

