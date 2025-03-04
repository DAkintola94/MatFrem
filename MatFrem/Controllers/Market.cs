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
        public IActionResult Index(string deliveryAddress, string paymentMethod, ShoppingCartViewModel scViewModel) //values that will be sent to ConfirmPurchase method, effective since we arent saving the data in the database
        {
            List<OrderItem> cartItems = CartHelper.GetCartItems(Request, Response, _context); //get the cart items from the cookie, through the CartHelper class/service
            decimal subtotal = CartHelper.GetSubTotal(cartItems);


            int cartSize = CartHelper.GetCartSize(Request, Response);

            var currentUser = _userManager.GetUserAsync(User).Result;

            foreach (var items in cartItems) //this is to get product values that is already inside our shoppingcart, since its a collection, we need to get values inside like this.
            {
                var productsElement = items.Product;
                //scViewModel.CartSize += items.Quantity;
                scViewModel.ProductNames.Add(productsElement.ProductName);
                scViewModel.PickUpAddress.Add(productsElement.ProductLocation);
                scViewModel.ProductDescription.Add(productsElement.Description);
                scViewModel.ProductCategories.Add(productsElement.ProductCategory);
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
            var cartById = await _shoppingCartRepository.GetCartById(id); 
            if (cartById != null)
            {
                var cartViewModel = new ShoppingCartViewModel 
                {
                    //ShoppingCartID = cartById.ShoppingCartID,
                    //CustomerId = cartById.CustomerId,
                    //ProductID = cartById.ProductID,
                    //ProductModel = cartById.Product.FirstOrDefault(), //set the single ProductModel property
					//Product = cartById.Product.ToList() ?? new List<ProductModel>() //Ensure product is not null
                };

                return View(cartViewModel); //we are returning the view model to the view
            }
            return View();
        }


        [HttpPost]
		public async Task<ActionResult> Cart(ProductViewModel productViewModel)
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
                    ProductID = productViewModel.ProductID,
                    ProductName = productViewModel.ProductViewName,
                    ProductPrice = productViewModel.ProductViewPrice,
                    ProductLocation = productViewModel.ProductViewLocation,
                    CustomerName = currentSubmitter.FirstName + currentSubmitter.LastName,
                };

                await _productRepository.InsertProduct(convertPModel);

                return RedirectToAction("ActiveDeliveries", "Driver", convertPModel);
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> ConfirmPurchase(ShoppingCartViewModel scViewModel) //values we are retrieving from index method above
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

            //since we have already gotten our model sent from another method (index), we can just attach the values to the order and add to the database. 
            
            if(scViewModel.ProductNames.Count >= 2)
            {
                OrderModel orderMultiModel = new OrderModel
                {
                    ProductNames = scViewModel.ProductNames.ToList(),



                };

                 await _orderRepository.AddOrder(orderMultiModel);
                Response.Cookies.Delete("shopping_cart"); //delete the cookie after the order is placed

                return View(scViewModel); //returning the viewmodel to the view, which is sent from the index method
            }

            else
            {
                OrderModel orderSingelModel = new OrderModel
                {
                    ProductName = scViewModel.ProductNames.FirstOrDefault(),
                    CustomerPhoneNr = scViewModel.CustomerPhoneNr,
                    CustomerName = scViewModel.CustomerName,
                    OrderCreatedDate = DateOnly.FromDateTime(DateTime.Now),
                    PickUpAddress = scViewModel.PickUpAddress,
                    OrderStatusID = 1, //Setting the order status to 1 (default, when the order is purchased), which is "Order received"
                    OrderItem = scViewModel.CartSize,
                    TotalPrice = scViewModel.Total,
                    PaymentMethod = scViewModel.PaymentMethod,
                    ProductCategory = scViewModel.ProductCategories,
                    DeliveryAddress = scViewModel.DeliveryAddress
                };

                await _orderRepository.AddOrder(orderSingelModel);
                Response.Cookies.Delete("shopping_cart"); //delete the cookie after the order is placed

                return View(scViewModel); //returning the viewmodel to the view, which is sent from the index method


            }

            return BadRequest("Something went wrong");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
           var deleteItem = await _productRepository.DeleteItem(id);
            return View();
        }

     
    }
}

