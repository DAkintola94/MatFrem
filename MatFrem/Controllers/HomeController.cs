using MatFrem.Models;
using MatFrem.Models.DomainModel;
using MatFrem.Models.ViewModel;
using MatFrem.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MatFrem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdviceRepository _adviceRepository;

		public HomeController(ILogger<HomeController> logger, IAdviceRepository adviceRepo)
        {
            _logger = logger;
            _adviceRepository = adviceRepo;
		}

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Advice()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Advice(AdviceViewModel adviceModel)
        {
            AdviceModel viewModel = new AdviceModel
            {
                Author = adviceModel.Author,
                AdviceMessage = adviceModel.AdviceMessage,
                Email = adviceModel.Email,
                Date = adviceModel.Date

            };

            if(ModelState.IsValid)
            {
                var viewModelResult = await _adviceRepository.AddAdvice(viewModel);
                return RedirectToAction("FeedbackNotis");
            }

            return RedirectToAction("Home");
        }

        [HttpGet]
		public IActionResult FeedbackNotis()
		{
			return View();
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
