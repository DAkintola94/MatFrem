﻿using MatFrem.Models.DomainModel;
using MatFrem.Models.ViewModel;
using MatFrem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
					OrderStatusDescription = o.OrderStatus?.StatusDescription ?? string.Empty, //We are attaching the outcoming data (CHAR), status description, to OrderStatus.StatusDescription
                                                                                               //That is the foreign key in the OrderModel, that is connected to the OrderStatusModel
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
		public async Task<IActionResult> OrderOverview(int id)
		{
			
			var getOrders = await _orderRepository.GetOrderByID(id);
			var currentUser = await _userManager.GetUserAsync(User); 

            if (getOrders != null && currentUser!= null)
			{
				var response  = await _httpClient.GetAsync($"https://localhost:7156/api/location/GetLocation?address={getOrders.DeliveryAddress}");
				if(response.IsSuccessStatusCode)
				{
					var geoJson = await response.Content.ReadAsStringAsync();
					var geoData = JsonConvert.DeserializeObject<GeoJson>(geoJson);
                    var deliveryAddress = geoData?.Properties?.Address ?? string.Empty;
					Console.WriteLine(deliveryAddress);
					Console.WriteLine(geoData);

                    OrderViewModel orderViewModel = new OrderViewModel //we can simply attach to view because GetORderById is not ienumerable (list) in the repository
				{
					OrderID = getOrders.OrderID, //need to pass and attach the ID from DB to the view model so we can work with the primary key/id
												 //Since this does not auto connect when dealing with view model							 
					CustomerName = getOrders.CustomerName ?? string.Empty,
					CustomerPhoneNr = getOrders.CustomerPhoneNr ?? string.Empty,
					ProductName = getOrders.ProductName ?? string.Empty,
					TotalAmount = getOrders.TotalPrice,
					OrderQuantitySize = getOrders.OrderItem,
					OrderStatusID = getOrders.OrderStatusID,
					OrderStatusDescription = getOrders.OrderStatus?.StatusDescription ?? string.Empty, //this works because of eager loading in repository
					PickUpAddress = getOrders.PickUpAddress ?? string.Empty,
					ItemCategory = getOrders.ProductCategory ?? string.Empty,
					DateOrderCreate = getOrders.OrderCreatedDate,
					DeliveryAddress = getOrders.DeliveryAddress ?? string.Empty,

                    GeoJson = deliveryAddress, //attaching the converted address from api to this view model type

                    DriverName = currentUser.FirstName + " " + currentUser.LastName //attaching the driver name to the current driver(user) logged in
                };

                return View(orderViewModel); //pass the model we have attached to values from the DB, and send it to view
            }
        }

            return NotFound(); //if the order is not found, return not found
        }

		[HttpPost]
		public async Task<IActionResult> StartOrder(int id, int? any) //The start and finish order method are simply methods, no need to create a view for them
        {
            var getOrders = await _orderRepository.GetOrderByID(id);  

            var currentUser = await _userManager.GetUserAsync(User); //we can just use this since its only driver than can use this controller/site

            if (getOrders == null)
            {
                return View();
            }
            //after getting order by id from database, we change specific values then update the order

            getOrders.OrderStatusID = 3; //we are changing the status of the order to 2, which is "On the way"
            getOrders.DriverId = currentUser.Id; //we attaching the "currentuser, aka driver" to the order, only driver can use this method anyway
            getOrders.Driver = currentUser; 

            await _orderRepository.UpdateOrder(getOrders);
            return RedirectToAction("OrderOverview", new {id}); //redirect to the OrderOverview page, with the id of the order
        }

		[HttpPost]
		public async Task<IActionResult> FinishOrder(int id) //The start and finish order method are simply methods, no need to create a view for them
        {
			var getOrders = await _orderRepository.GetOrderByID(id);
			var currentUser = await _userManager.GetUserAsync(User); //we can just use this since its only driver than can use this controller/site

			if (getOrders == null)
			{
				return View();
			}

			getOrders.OrderStatusID = 4;
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

        //[Authorize(Roles = "System Administrator")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var findOrder = await _orderRepository.GetOrderByID(id);
            if (findOrder == null)
            {
                return NotFound();
            }

            await _orderRepository.DeleteOrder(id);
            return RedirectToAction("OrderOverview");
        }


        [HttpPost]
        public async Task<IActionResult> CancelOrder(int id) //The start and finish order method are simply methods, no need to create a view for them
        {
            var getOrders = await _orderRepository.GetOrderByID(id);
            var currentUser = await _userManager.GetUserAsync(User); //we can just use this since its only driver than can use this controller/site

            if (getOrders == null)
            {
                return View();
            }

            getOrders.OrderStatusID = 5;
            getOrders.DriverId = currentUser.Id;
            getOrders.Driver = currentUser;

            await _orderRepository.UpdateOrder(getOrders);
            return RedirectToAction("YourOrder");
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

