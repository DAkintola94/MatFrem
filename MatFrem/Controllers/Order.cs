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
    }
}
