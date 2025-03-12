using MatFrem.Models.DomainModel;
using MatFrem.Models.ViewModel;
using MatFrem.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MatFrem.Controllers
{
    public class Order : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public Order(IOrderRepository orderRepo, UserManager<ApplicationUser> userManager)
        {
            _orderRepository = orderRepo;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<ActionResult> OrderDetails(int id)
        {
            var getOrderById = await _orderRepository.GetOrderByID(id);
            if (getOrderById != null)
            {
                OrderViewModel orderViewModel = new OrderViewModel
                {
                    //CustomerName = getOrderById.CustomerName,
                    //CustomerPhoneNr = getOrderById.CustomerPhoneNr,
                    //OrderID = getOrderById.OrderID,
                    //ProductNames = getOrderById.ProductName,


                    //ProductNames = getOrderById.OrderProduct.Select(op => op.ProductM.ProductName).ToList(),
                    //ProductCategories = getOrderById.OrderProduct.Select(op => op.ProductM.ProductCalories).ToList()
                };

                return View(orderViewModel);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<ActionResult> OrderDetails(OrderModel orderModel)
        {
            if(orderModel != null)
            {
                await _orderRepository.UpdateOrder(orderModel);
                return RedirectToAction("OrderDetails");
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> OrderHistory(int pageSize = 8, int pageNumber = 1)
        {

            var totalRecords = await _orderRepository.CountPage();
            var totalPages = (int)Math.Ceiling((decimal)totalRecords / pageSize);

            pageNumber = Math.Clamp(pageNumber, 1, totalPages);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.PageSize = pageSize;

            var getOrders = await _orderRepository.GetAllOrder(pageNumber, pageSize);
            var currentUser = await _userManager.GetUserAsync(User);

            if (getOrders != null && currentUser != null)
            {
                    var orderViewModel = getOrders
                    .Where(DtoIdMatch => DtoIdMatch.CustomerPhoneNr == currentUser.PhoneNumber) //this is linq to check if the customer phone nr in the database, matches who is currently logged in. 
                                                                              //If it does, we will return the order details that belongs to the customer

                    .Select(o => new OrderViewModel //getOrders is already taking from db OrderModel, now we simply map it to OrderViewModel
                    {
                        OrderID = o.OrderID,
                        OrderViewProductNames = o.ProductNames.ToList(), //eagerloading, to list because we are getting a list and sending to view 
                        TotalAmount = o.TotalPrice,
                        OrderQuantitySize = o.OrderItem,
                        OrderStatusDescription = o.OrderStatus?.StatusDescription, //this works due to eager loading in the repository
                        DateOrderCreate = o.OrderCreatedDate,
                        DeliveryAddress = o.DeliveryAddress,
                        PaymentType = o.PaymentMethod,
                    }).ToList();

                    if(orderViewModel.Any()) //you need to use a loop or LINQ to get properties in a list
                    {
                            return View(orderViewModel);
                    }
            }
            return NotFound();
        }
    }
}
