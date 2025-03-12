using MatFrem.Models.DomainModel;
using MatFrem.Models.ViewModel;
using MatFrem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace MatFrem.Controllers
{
	//[Authorize(Roles = "Driver")] //instead of using the [Authorize] attribute on every method, you can use it on the class.
	//Every method need to be authorized to be accessed
	public class Driver : Controller
	{
		private readonly IProductRepository _productRepository;

		private readonly IOrderRepository _orderRepository;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly HttpClient _httpClient;

		public Driver(IProductRepository productRepo,
			IOrderRepository orderRepository, UserManager<ApplicationUser> userManager,
			HttpClient httpClient)
		{
			_productRepository = productRepo;
			_orderRepository = orderRepository;
			_userManager = userManager;
            _httpClient = httpClient;
        }
		[HttpGet]

		public async Task<IActionResult> OrderMapView()
		{
            var getOrders = await _orderRepository.GetAllOrder();

            if (getOrders != null)
            {

                //Here, we just attach our values in the viewmodel, to what is in the database, then send it to the view. 
                //We are in the GET method, so we are just getting the data from the database, and sending it to the view. This happens when the page is loaded.

                var orderViewModels = getOrders.Select(o => new OrderViewModel //need to run through the list and select since its ienumerable in the repository
                {
                    OrderID = o.OrderID, //need to pass and attach the ID from DB to the view model, remember that when working with view model
                    CustomerName = o.CustomerName ?? string.Empty,
                    CustomerPhoneNr = o.CustomerPhoneNr ?? string.Empty,
                    TotalAmount = o.TotalPrice,
                    OrderQuantitySize = o.OrderItem,
                    OrderStatusDescription = o.OrderStatus?.StatusDescription ?? string.Empty, //We are attaching the value from the database, which is from another table, then sending it to the view
                                                                                               //That is the foreign key in the OrderModel, that is connected to the OrderStatusModel
                    PickUpAddress = o.PickUpAddress ?? string.Empty,
                    DateOrderCreate = o.OrderCreatedDate,
                    DeliveryAddress = o.DeliveryAddress ?? string.Empty,
                    OrderViewProductNames = o.ProductNames.ToList(),
                    OrderviewProductDescription = o.ProductDescription.ToList(),
                    ItemCategory = o.ProductCategories.ToList(),
                    ViewGeoJson = o.ProductGeoJson.ToList(),

                }).ToList();
                return View(orderViewModels);
            }
            return BadRequest("Error, something went wrong");

        }


		public async Task<IActionResult> DriverPage(int pageSize = 8, int pageNumber = 1)
		{
				var totalRecords = await _productRepository.CountPage();
				var totalPages = totalRecords == 0 ? 1 : (int)Math.Ceiling((decimal)totalRecords / pageSize);

            pageNumber = Math.Clamp(pageNumber, 1, totalPages);

				ViewBag.TotalPages = totalPages;
				ViewBag.CurrentPage = pageNumber;
				ViewBag.PageSize = pageSize;

				var getOrders = await _orderRepository.GetAllOrder(pageNumber, pageSize); //variable to get the order from database upon loading the page

            if (getOrders != null)
            {
				//Here, we attach the data from the database, to the view model. Then send it to view later. As a list 

				var orderViewModels = getOrders.Select(o => new OrderViewModel //need to run through the list and select since its ienumerable in the repository
                {
					OrderID = o.OrderID, //need to pass and attach the ID from DB to the view model, remember that when working with view model
					CustomerName = o.CustomerName ?? string.Empty,
					CustomerPhoneNr = o.CustomerPhoneNr ?? string.Empty,
					TotalAmount = o.TotalPrice,
					OrderQuantitySize = o.OrderItem,
					OrderStatusDescription = o.OrderStatus?.StatusDescription ?? string.Empty, //We are attaching the outcoming data (CHAR), status description, to OrderStatus.StatusDescription
                                                                                               //That is the foreign key in the OrderModel, that is connected to the OrderStatusModel
                    PickUpAddress = o.PickUpAddress ?? string.Empty,
					DateOrderCreate = o.OrderCreatedDate,
					DeliveryAddress = o.DeliveryAddress ?? string.Empty,
                    OrderViewProductNames = o.ProductNames.ToList(),
					OrderviewProductDescription = o.ProductDescription.ToList(),
                    ItemCategory = o.ProductCategories.ToList(),
                    ViewGeoJson = o.ProductGeoJson.ToList(),
					
                }).ToList();
                return View(orderViewModels);
            }
            return BadRequest("Error, something went wrong");
        }

		[HttpGet]
		public async Task<IActionResult> OrderOverview(int id)
		{
			
			var getOrders = await _orderRepository.GetOrderByID(id);
			var currentUser = await _userManager.GetUserAsync(User); 

            if (getOrders != null && currentUser!= null)
			{
                    OrderViewModel orderViewModel = new OrderViewModel //we can simply attach to view because GetORderById is not ienumerable (list) in the repository
				{
					OrderID = getOrders.OrderID, //need to pass and attach the ID from DB to the view model so we can work with the primary key/id
												 //Since this does not auto connect when dealing with view model							 
					CustomerName = getOrders.CustomerName ?? string.Empty,
					CustomerPhoneNr = getOrders.CustomerPhoneNr ?? string.Empty,
					OrderViewProductNames = getOrders.ProductNames.ToList(),
					TotalAmount = getOrders.TotalPrice,
					OrderQuantitySize = getOrders.OrderItem,
					OrderStatusID = getOrders.OrderStatusID,
					OrderStatusDescription = getOrders.OrderStatus?.StatusDescription ?? string.Empty, //this works because of eager loading in repository
					PickUpAddress = getOrders.PickUpAddress ?? string.Empty,
					ItemCategory = getOrders.ProductCategories.ToList(),
					DateOrderCreate = getOrders.OrderCreatedDate,
					ViewGeoJson = getOrders.ProductGeoJson.ToList(), //geojson is stored as single element 
					OrderviewProductDescription = getOrders.ProductDescription.ToList(),
					DeliveryAddress = getOrders.DeliveryAddress ?? string.Empty,
                    DriverName = currentUser.FirstName + " " + currentUser.LastName //attaching the driver name to the current driver(user) logged in
                };

                return View(orderViewModel); //pass the model we have attached to values from the DB, and send it to view
            }

            return NotFound(); //if the order is not found, return not found
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
                    //not completed yet
                };

				return View(orderModel);
			}
			return View();
		}


		public async Task<IActionResult> DeliveryHistory()
		{
            var getOrders = await _orderRepository.GetAllOrder();
            var currentUser = await _userManager.GetUserAsync(User);

            if (getOrders != null)
            {
                var orderViewModels = getOrders.Where(pNrDTO => pNrDTO.DriverId == currentUser.PhoneNumber) //this is linq to check if the driver phone from database matches the current user logged in phone number
                    .Select(o => new OrderViewModel
                {
                    CustomerName = o.CustomerName ?? string.Empty,
                    CustomerPhoneNr = o.CustomerPhoneNr ?? string.Empty,
                    OrderViewProductNames = o.ProductNames.ToList(),
                    TotalAmount = o.TotalPrice,
                    OrderStatusDescription = o.OrderStatus?.StatusDescription ?? string.Empty,
                    PickUpAddress = o.PickUpAddress ?? string.Empty,
                    ItemCategory = o.ProductCategories.ToList(),
                    DateOrderCreate = o.OrderCreatedDate,
                    DeliveryAddress = o.DeliveryAddress ?? string.Empty
                }).ToList();
                return View(orderViewModels); //Pass the model we have attached to values from the DB, to view. Its as a list
                                               //We need to get the value as a list in view as well.
                                              //Then loop through it
            }
            return BadRequest("Invalid request, no order found");
        }

        [HttpPost]
        public async Task<IActionResult> FinishOrder(int id) //The start and finish order method are simply methods, no need to create a view for them
        {
            var getOrders = await _orderRepository.GetOrderByID(id);
            var currentUser = await _userManager.GetUserAsync(User); //we can just use this since its only driver than can use this controller/site

            if (getOrders != null && currentUser != null)
            {
                getOrders.OrderStatusID = 4;
                getOrders.DriverId = currentUser.PhoneNumber;

                await _orderRepository.UpdateOrder(getOrders);
                return RedirectToAction("YourOrder");
            }

            return BadRequest("Invalid request");
        }

        [HttpPost]
        public async Task<IActionResult> StartOrder(int id, int? any) //The start and finish order method are simply methods, no need to create a view for them
        {
            var getOrders = await _orderRepository.GetOrderByID(id);

            var currentUser = await _userManager.GetUserAsync(User);

            if (getOrders != null && currentUser != null)
            {
                getOrders.OrderStatusID = 3; //we are changing the status of the order to 2, which is "On the way"
                getOrders.DriverId = currentUser.PhoneNumber;

                await _orderRepository.UpdateOrder(getOrders);
                return RedirectToAction("OrderOverview", new { id }); //redirect to the OrderOverview page, with the id of the order
            }

            return BadRequest("No order or user found");
        }

        [HttpPost]
        public async Task<IActionResult> CancelOrder(int id) //The start and finish order method are simply methods, no need to create a view for them
        {
            var getOrders = await _orderRepository.GetOrderByID(id);
            var currentUser = await _userManager.GetUserAsync(User); //we can just use this since its only driver than can use this controller/site

            if (getOrders != null && currentUser != null)
            {
                getOrders.OrderStatusID = 5;
                getOrders.DriverId = currentUser.PhoneNumber;

                await _orderRepository.UpdateOrder(getOrders);
                return RedirectToAction("YourOrder");
            }

			return BadRequest("No user or order found");
        }


        [Authorize(Roles = "System Administrator")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderRepository.DeleteOrder(id);
            return RedirectToAction("OrderViewMap");
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

