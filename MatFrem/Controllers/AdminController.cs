using MatFrem.Models.ViewModel;
using MatFrem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            

            var getAllUsers = await _userRepository.GetAllUsers();

            if(getAllUsers!= null)
            {
                var profileViewModel = getAllUsers.Select(userList => new CreateProfileViewModel
                {
                    Email = userList.Email,
                    PhoneNr = userList.PhoneNumber,
                    FirstName = userList.FirstName,
                    LastName = userList.LastName
                }).ToList();

                return View(profileViewModel);
            }

            //also, using the same viewmodel as for registering user, because our modal needs the same attributes that are required etc
            return BadRequest("No user found");
        }

        [HttpPost]
        public async Task<IActionResult> UserOverview(CreateProfileViewModel createProfileViewModel)
        {
            return null;


        }





    }
}
