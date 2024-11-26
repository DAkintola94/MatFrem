using MatFrem.DataContext;
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
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IConfiguration _configuration; //IConfiguration is a built-in interface in .NET Core that provides access to the appsettings.json file
        private readonly AppDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager; //UserManager is a built-in class in .NET Core that provides all the functionality needed to manage users in the system
        private readonly decimal deliveryFee; 

        public Market(IProductRepository productRepo, 
            UserManager<ApplicationUser> userManager, IOrderRepository orderRepository,
            IShoppingCartRepository shoppingCartRepository, IConfiguration configuration 
            , AppDBContext context)
        {
            _context = context;
            _configuration = configuration;
            _productRepository = productRepo;
            _userManager = userManager;
            _orderRepository = orderRepository;
            _shoppingCartRepository = shoppingCartRepository;
            deliveryFee = configuration.GetValue<decimal>("CartSettings:DeliveryFee"); //get the delivery fee from the appsettings.json file
        }


        [HttpGet]
        public IActionResult Index()
        {
               List<OrderItem> cartItems = CartHelper.GetCartItems(Request, Response, _context);
            decimal subtotal = CartHelper.GetSubTotal(cartItems);

            ShoppingCartViewModel scViewModel = new ShoppingCartViewModel
            {
                CartItems = cartItems,
                Subtotal = subtotal,
                DeliveryFee = deliveryFee,  
                Total = subtotal + deliveryFee
            }; //Creating  ShoppingCartViewModel and giving it the values from the cookie, and OrderItem list

            return View(scViewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Index(string deliveryAddress, string paymentMethod)
        {
            List<OrderItem> cartItems = CartHelper.GetCartItems(Request, Response, _context); //get the cart items from the cookie, through the CartHelper class/service
            decimal subtotal = CartHelper.GetSubTotal(cartItems);

            
            ShoppingCartViewModel scViewModel = new ShoppingCartViewModel
            {
                CartItems = cartItems,
                Subtotal = subtotal,
                DeliveryFee = deliveryFee,
                Total = subtotal + deliveryFee,
                DeliveryAddress = deliveryAddress, //attaching it to the name="" defined in html, getting data through the parameter here
                PaymentMethod = paymentMethod //attaching it to the name="" defined in html, getting data through the parameter here
            };
            
            if (!ModelState.IsValid)
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
            
			return View(listAllProducts);
        }

        [HttpGet]
        public async Task<ActionResult> Cart(int id)
        {
            var cartById = await _shoppingCartRepository.GetCartById(id); 
            if (cartById != null)
            {
                var cartViewModel = new ShoppingCartViewModel 
                {
                    ShoppingCartID = cartById.ShoppingCartID,
                    CustomerId = cartById.CustomerId,
                    ProductID = cartById.ProductID,
                    ProductModel = cartById.Product.FirstOrDefault(), //set the single ProductModel property
					Product = cartById.Product.ToList() ?? new List<ProductModel>() //Ensure product is not null
                };

                return View(cartViewModel); //we are returning the view model to the view
            }
            return View();
        }


        [HttpPost]
		public async Task<ActionResult> Cart(ProductViewModel request)
		{
			if(!ModelState.IsValid)
            {
                return View();
            }

            var currentSubmitter = await _userManager.GetUserAsync(User); //we are getting the current user that is logged in
                                                                          ////getuserasync gets all user details (all properties).
            if (currentSubmitter != null)
            {
                ProductModel convertPModel = new ProductModel
                {
                    ProductID = request.ProductID,
                    ProductName = request.ProductName,
                    ProductPrice = request.ProductPrice,
                    ProductLocation = request.ProductLocation,
                    CustomerId = currentSubmitter.Id,
                };

                await _productRepository.InsertProduct(convertPModel);

                return RedirectToAction("ActiveDeliveries", "Driver", convertPModel);
            }
            return View();
        }


       

        [HttpGet]
        public async Task<ActionResult> ConfirmPurchase(ShoppingCartViewModel scViewModel)
        {
            List<OrderItem> cartItems = CartHelper.GetCartItems(Request, Response, _context); //get the cart items from the cookie, through the CartHelper class/service
            decimal total = CartHelper.GetSubTotal(cartItems) + deliveryFee;
            int cartSize = 0;

            foreach (var item in cartItems)
            {
                cartSize += item.Quantity;
            }

            

            if(cartSize == 0 || string.IsNullOrEmpty(scViewModel.DeliveryAddress) || string.IsNullOrEmpty(scViewModel.PaymentMethod))
            {
                return RedirectToAction("Index", "Home");
            }

            scViewModel.CartItems = cartItems;
            scViewModel.Total = total;
            scViewModel.CartSize = cartSize;

            // scViewModel.PaymentMethod is already passed and sent here from Index method above [Post], no need to assign it here
            // scViewModel.DeliveryAddress is already passed and sent here from Index method above [Post], no need to assign it here

            return View(scViewModel);
        }


        [HttpPost]
        public async Task<ActionResult> ConfirmPurchase(ShoppingCartViewModel scViewModel, int any) //any does nothing, its just so our post method works
        {
            List<OrderItem> cartItems = CartHelper.GetCartItems(Request, Response, _context);

            if (cartItems.Count == 0 || string.IsNullOrEmpty(scViewModel.DeliveryAddress) || string.IsNullOrEmpty(scViewModel.PaymentMethod))
            {
                return RedirectToAction("Index", "Home");
            }

            var appUser = await _userManager.GetUserAsync(User);
            if(appUser == null)
            {
                return RedirectToAction("Login", "ProfileManagement");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            OrderModel orderModel = new OrderModel
            {
                CustomerId = appUser.Id,
                CustomerName = appUser.FirstName + " " + appUser.LastName,
                OrderCreatedDate = DateOnly.FromDateTime(DateTime.Now),
                OrderItems = cartItems,
                Order_Status = "Pending",
                DeliveryFee = deliveryFee,
                PaymentMethod = scViewModel.PaymentMethod, //getting the value from shoppingcartviewmodel, and placing it into order model, which have the same string "PaymentMethod"
                DeliveryAddress = scViewModel.DeliveryAddress, //getting the value from shoppingcartviewmodel, and placing it into order model which have the same string "DeliveryAddress"
            };

             //now we save the data we get and merge from the view models into database
             //When you want to get the order later, you simply get it by the order id from the database
             //And you have all info on who is attached to the order, etc

            await _orderRepository.AddOrder(orderModel);
            return View("Index", "Home");

        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
           var deleteItem = await _productRepository.DeleteItem(id);
            return View();
        }
    }
}

