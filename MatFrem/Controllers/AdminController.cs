using MatFrem.Models.ViewModel;
using MatFrem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

namespace MatFrem.Controllers
{
    [Authorize(Roles = "System Administrator")]
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AdminController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> UserOverview()
        {
            //the repository already handle the logic to remove the system admin from the list of users
            //working with sending list to view this way, so our MODAL can work with asp-for single property post
            

            var getAllUsers = await _userRepository.GetAllUsers();

            var adminViewModel = new AdminViewModel();
            adminViewModel.Users = new List<CreateProfileViewModel>();

            if(getAllUsers!= null)
            {
                foreach(var userElement in getAllUsers)
                {
                    adminViewModel.Users.Add(new Models.ViewModel.CreateProfileViewModel
                    {
                        Email = userElement.Email,
                        PhoneNr = userElement.PhoneNumber,
                        FirstName = userElement.FirstName,
                        LastName = userElement.LastName
                    });
                }

                return View(adminViewModel);
            }

            //also, using the same viewmodel as for registering user, because our modal needs the same attributes that are required etc
            return BadRequest("No user found");
        }

        [HttpPost]
        public async Task<IActionResult> UserOverview(AdminViewModel adminProfileRequest)
        {
            if(ModelState.IsValid)
            {
                return View();
            }

            return BadRequest("No value issued");

        }





    }
}
