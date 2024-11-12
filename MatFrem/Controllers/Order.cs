using MatFrem.Models.DomainModel;
using MatFrem.Models.ViewModel;
using MatFrem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MatFrem.Controllers
{
    public class Order : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public Order(IOrderRepository orderRepo)
        {
            _orderRepository = orderRepo;
        }
        public IActionResult OrderList()
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
                    OrderID = getOrderById.OrderID,
                    OrderCreatedDate = getOrderById.OrderCreatedDate,
					DriverID = getOrderById.DriverID,
					CustomerID = getOrderById.CustomerID,
					ShopID = getOrderById.ShopID,
					OrderStatusID = getOrderById.OrderStatusID
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
    }
}
