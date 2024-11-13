using MatFrem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MatFrem.Controllers
{
	//[Authorize(Roles = "Driver")] //instead of using the [Authorize] attribute on every method, you can use it on the class.
	                                //Every method need to be authorized to be accessed
	public class Driver : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ILocationRepository _locationRepository;

		public Driver(IProductRepository productRepo, ILocationRepository locationRepo)
        {
            _productRepository = productRepo;
			_locationRepository = locationRepo;
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

        public IActionResult ActiveDeliveries()
		{
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
