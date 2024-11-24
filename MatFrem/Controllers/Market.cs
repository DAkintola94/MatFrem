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
        private readonly IConfiguration _configuration;
        private readonly AppDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
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
            deliveryFee = configuration.GetValue<decimal>("CartSettings:DeliveryFee");
        }



        public IActionResult Index(string locationAddress)
        {
               List<OrderItem> cartItems = CartHelper.GetCartItems(Request, Response, _context);
            decimal subtotal = CartHelper.GetSubTotal(cartItems);

            ShoppingCartViewModel scViewModel = new ShoppingCartViewModel
            {
                CartItems = cartItems,
                Subtotal = subtotal,
                DeliveryFee = deliveryFee,
                DeliveryAddress = locationAddress,  //name="" in html, it then get sent to the parameter, which have the same name here
                Total = subtotal + deliveryFee

            };

            return View(scViewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Index(ShoppingCartViewModel shoppingCartView, string locationAddress)
        {
            List<OrderItem> cartItems = CartHelper.GetCartItems(Request, Response, _context);
            decimal subtotal = CartHelper.GetSubTotal(cartItems);

            shoppingCartView.CartItems = cartItems;
            shoppingCartView.Subtotal = subtotal;
            shoppingCartView.DeliveryFee = deliveryFee;
            shoppingCartView.DeliveryAddress = locationAddress;
            shoppingCartView.Total = subtotal + deliveryFee;
            

            if (!ModelState.IsValid)
            {
                return View(shoppingCartView);
            }

            if(cartItems.Count == 0)
            {
                ViewBag.ErrorMessage = "handlekurven er tom";
                return View(shoppingCartView);
            }
            return RedirectToAction("Buy", shoppingCartView);
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
            var cartById = await _shoppingCartRepository.GetCartById(id); //We are getting data from database by its id (primary key)
            if (cartById != null)
            {
                var cartViewModel = new ShoppingCartViewModel //this way, we attach the data model from db into the view model
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

        public async Task<ActionResult> Buy(int id)
        {
            return View();
        }

		[HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
           var deleteItem = await _productRepository.DeleteItem(id);
            return View();
        }
    }
}

