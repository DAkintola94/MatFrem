﻿using MatFrem.DataContext;
using MatFrem.Models.DomainModel;
using MatFrem.Models.ViewModel;
using MatFrem.Repository;
using MatFrem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace MatFrem.Controllers
{
    public class Market : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IConfiguration _configuration; //IConfiguration is a built-in interface in .NET Core that provides access to the appsettings.json file
        private readonly AppDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager; 
        private readonly decimal deliveryFee; 

        public Market(IProductRepository productRepo, 
            UserManager<ApplicationUser> userManager, IOrderRepository orderRepository,IConfiguration configuration ,AppDBContext context)
        {
            _context = context;
            _configuration = configuration;
            _productRepository = productRepo;
            _userManager = userManager;
            _orderRepository = orderRepository;
            deliveryFee = configuration.GetValue<decimal>("CartSettings:DeliveryFee"); //get the delivery fee from the appsettings.json file
        }

        [HttpGet]
        public IActionResult Index()
        {
               List<OrderItem> cartItems = CartHelper.GetCartItems(Request, Response, _context); //get the cart items from the cookie, through the CartHelper class/service

            int cartSize = CartHelper.GetCartSize(Request, Response); //variable to check cart size, GetCartSize method is from the CartHelper service


            if (cartSize >= 1) //if there is more than 1 item in the cart, we can show the cart
            {
                decimal subtotal = CartHelper.GetSubTotal(cartItems);

                ShoppingCartViewModel scViewModel = new ShoppingCartViewModel
                {
                    CartItems = cartItems,
                    Subtotal = subtotal,
                    DeliveryFee = deliveryFee,
                    Total = subtotal + deliveryFee
                }; //Creating new ShoppingCartViewModel and giving it the values from the cookie, and OrderItem list

                return View(scViewModel);
            }

            return BadRequest("There is no item in your cart");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Index(string deliveryAddress, string paymentMethod, ShoppingCartViewModel scViewModel) //data that will be sent to ConfirmPurchase method, effective since we arent saving the data in the database
        {
            List<OrderItem> cartItems = CartHelper.GetCartItems(Request, Response, _context); //get the cart items from the cookie, through the CartHelper class/service
            decimal subtotal = CartHelper.GetSubTotal(cartItems);


            int cartSize = CartHelper.GetCartSize(Request, Response);

            var currentUser = _userManager.GetUserAsync(User).Result;

            foreach (var items in cartItems) //This is to get the data from the cookie, then attach it to the viewmodel. The data will also be sent further
            {
                var productsElement = items.Product;
                //scViewModel.CartSize += items.Quantity;
                scViewModel.ProductNames.Add(productsElement.ProductName); //adding the values as list, so we can see several products in the view
                scViewModel.PickUpAddress.Add(productsElement.ProductLocation);
                scViewModel.ProductDescription.Add(productsElement.Description);
                scViewModel.ProductCategories.Add(productsElement.ProductCategory);
                scViewModel.GeoJsonView.Add(productsElement.GeoJson);
            }

            scViewModel.CartSize = cartSize; //this is to get and attach the values from the cartitems foreach loop above
			scViewModel.CartItems = cartItems; //this is to get the objects in the cartItems service, in line 59
			scViewModel.Subtotal = subtotal;
            scViewModel.DeliveryFee = deliveryFee;
            scViewModel.Total = subtotal + deliveryFee;
            scViewModel.DeliveryAddress = deliveryAddress; //attaching it to the name="" defined in html, getting data through the parameter here
            scViewModel.PaymentMethod = paymentMethod; //attaching it to the name="" defined in html, getting data through the parameter here
            scViewModel.CustomerName = currentUser.FirstName + " " + currentUser.LastName;
            scViewModel.CustomerPhoneNr = currentUser.PhoneNumber;


            //scViewModel html already have a form that takes in productname, id, details etc, its being sent to confirmpurchase


            if (scViewModel == null)
            {
                return View(scViewModel);
            }

            if(cartItems.Count == 0)
            {
                ViewBag.ErrorMessage = "handlekurven er tom";
                return View(scViewModel);
            }
            return RedirectToAction("ConfirmPurchase", scViewModel); //redirecting and sending model data to ConfirmPurchase
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

            if(listAllProducts != null)
            {
                var productViewModels = listAllProducts.Select(p => new ProductViewModel
                {
                    ProductID = p.ProductID,
                    ProductViewName = p.ProductName,
                    ProductViewPrice = p.ProductPrice,
                    ProductViewCalories = p.ProductCalories,
                    ProductViewLocation = p.ProductLocation,
                    ViewCategoryName = p.ProductCategory
                });
            }
            
			return View(listAllProducts);
        }

        [HttpGet]
        public async Task<ActionResult> Cart(int id)
        {
            return View();
        }


        [HttpPost]
		public async Task<ActionResult> Cart(ProductViewModel productViewModel)
		{
            var currentSubmitter = await _userManager.GetUserAsync(User); //we are getting the current user that is logged in
                                                                          //getuserasync gets all user details (all properties).
            if (currentSubmitter != null && productViewModel != null)
            {
                ProductModel convertPModel = new ProductModel
                {
                    ProductID = productViewModel.ProductID,
                    ProductName = productViewModel.ProductViewName,
                    ProductPrice = productViewModel.ProductViewPrice,
                    ProductLocation = productViewModel.ProductViewLocation,
                    CustomerName = currentSubmitter.FirstName + currentSubmitter.LastName,
                };

                await _productRepository.InsertProduct(convertPModel);

                return RedirectToAction("ActiveDeliveries", "Driver", convertPModel);
            }

            return NotFound("Error, invalid request");
        }

        [HttpGet]
        public async Task<ActionResult> ConfirmPurchase(ShoppingCartViewModel scViewModel) //values we are retrieving from index method above
        {
            List<OrderItem> cartItems = CartHelper.GetCartItems(Request, Response, _context); //get the cart items from the cookie, through the CartHelper class/service

            decimal total = CartHelper.GetSubTotal(cartItems) + deliveryFee;

            int cartSize = CartHelper.GetCartSize(Request, Response); //variable for method that is in the service, CartHelper. To check cart size

            
            if(cartSize == 0 || string.IsNullOrEmpty(scViewModel.DeliveryAddress) || string.IsNullOrEmpty(scViewModel.PaymentMethod))
            {
                return BadRequest("There is nothing in the cart");
            }

            //since we have already gotten our model sent from another method (index), we can just attach the values to the order and add to the database. 
            //NB! you can swap from different models, as long as the properties and data types are the same!!

            
                OrderModel orderModel = new OrderModel
                {
                    ProductNames = scViewModel.ProductNames.ToList(),
                    ProductCategories = scViewModel.ProductCategories.ToList(),
                    ProductDescription = scViewModel.ProductDescription.ToList(),
                    ProductGeoJson = scViewModel.GeoJsonView.ToList(),
                    CustomerPhoneNr = scViewModel.CustomerPhoneNr,
                    CustomerName = scViewModel.CustomerName,
                    OrderCreatedDate = DateOnly.FromDateTime(DateTime.Now),
                    ProductAddress = scViewModel.PickUpAddress.ToList(),
                    OrderStatusID = 1, //Setting the order status to 1 (default, when the order is purchased), which is "Order received"
                    OrderItem = scViewModel.CartSize,
                    TotalPrice = scViewModel.Total,
                    PaymentMethod = scViewModel.PaymentMethod,
                    DeliveryAddress = scViewModel.DeliveryAddress,
                };

                 await _orderRepository.AddOrder(orderModel);
                Response.Cookies.Delete("shopping_cart"); //delete the cookie after the order is placed

                return View(scViewModel); //returning the viewmodel to the view, which is sent from the index method
      
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
           var deleteItem = await _productRepository.DeleteItem(id);
            return View();
        }
       
        [HttpPost]
        public async Task<ActionResult> RemoveCookieCart()
        {
            int cartSize = CartHelper.GetCartSize(Request, Response); //get the cart size from the cookie, method from the CartHelper service

            if (cartSize >= 1)
            {
                Response.Cookies.Delete("shopping_cart");
                return RedirectToAction("Index");
            }

            return BadRequest("Shopping cart is already empty"); 

        }

     
    }
}

