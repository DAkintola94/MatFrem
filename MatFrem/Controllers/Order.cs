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
        public async Task<ActionResult> ActiveDeliveries(OrderViewModel orderViewModel)
        {
            return View();

        }

        [HttpGet]
        public IActionResult OrderTableDetails()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> OrderDetails(int id)
        {
            var getOrderById = await _orderRepository.GetOrderByID(id);
            if (getOrderById != null)
            {
                OrderViewModel orderViewModel = new OrderViewModel
                {
                   
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
        public async Task<ActionResult> OrderHistory()
        {
            var getOrders = await _orderRepository.GetAllOrder();
            var currentUser = await _userManager.GetUserAsync(User);

            if (getOrders != null && currentUser != null)
            {
                    var orderViewModel = getOrders
                    .Where(o => o.CustomerId == currentUser.Id) //this is linq to check if the customer id matches. 
                                                                //also, linq or loop is needed to get properties in a list
                    .Select(o => new OrderViewModel //getOrders is already taking from db OrderModel, now we simply map it to OrderViewModel
                    {
                        CustomerId = o.CustomerId,
                        CustomerName = o.CustomerName,
                        OrderID = o.OrderID,
                        ProductName = o.ProductName,
                        TotalAmount = o.TotalPrice,
                        DateOrderCreate = o.OrderCreatedDate,
                        OrderStatusDescription = o.OrderStatus?.StatusDescription,
                        DeliveryAddress = o.DeliveryAddress
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
