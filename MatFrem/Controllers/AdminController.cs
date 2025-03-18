using MatFrem.Models.DomainModel;
using MatFrem.Models.ViewModel;
using MatFrem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

namespace MatFrem.Controllers
{
    [Authorize(Roles = "System Administrator")]
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> UserOverview()
        {
            //the repository already handle the logic to remove the system admin from the list of users
            //working with sending list to view this way, so our MODAL can work with asp-for single property post
            

            var getAllUsers = await _userRepository.GetAllUsers();

            var adminViewModel = new AdminViewModel(); 

            adminViewModel.ProfileCreation = new List<CreateProfileViewModel>(); 

            if(getAllUsers!= null)
            {
                foreach(var userElement in getAllUsers)
                {
                    adminViewModel.ProfileCreation.Add(new Models.ViewModel.CreateProfileViewModel
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
            if(adminProfileRequest!= null)
            {
                ApplicationUser applicationUser = new ApplicationUser
                {
                    Email = adminProfileRequest.UserEmail,
                    PhoneNumber = adminProfileRequest.UserPhoneNr,
                    FirstName = adminProfileRequest.UserFirstName,
                    LastName = adminProfileRequest.UserLastName
                };

                var applicationResult = await _userManager.CreateAsync(applicationUser, adminProfileRequest.Password);

                if(applicationResult.Succeeded && adminProfileRequest.IsDriver == true)
                {
                    var applicationIdentityResult = await _userManager.AddToRoleAsync(applicationUser, "Driver");

                    if(applicationIdentityResult.Succeeded)
                    {
                        return RedirectToAction("Login", "ProfileManagement");
                    }

                }

                else if (applicationResult.Succeeded && adminProfileRequest.IsCustomer == true)
                {
                    var applicationIdentityResult = await _userManager.AddToRoleAsync(applicationUser, "Customer");
                    if (applicationIdentityResult.Succeeded)
                    {
                        return RedirectToAction("Login", "ProfileManagement");
                    }
                }

                else
                {
                    return BadRequest("No valid role avaliable");
                }

            }

            return BadRequest("Ops, something went wrong");

        }

    }
}
