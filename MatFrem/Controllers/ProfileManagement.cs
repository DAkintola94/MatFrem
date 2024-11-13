using MatFrem.Models.DomainModel;
using MatFrem.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace MatFrem.Controllers
{
   
    public class ProfileManagement : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager; //we using a custom model instead of default IdentityUser
        private readonly SignInManager<ApplicationUser> _signInManager;
        public ProfileManagement(UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult CreateProfile()
        {
            return View();
        }


		/// <summary>
		/// Creates a new user profile based on registration form input and assigns the "Customer" role if successful.
		/// </summary>
		/// <param name="createProfile">The view model containing user registration details, such as username, first name, last name, email, phone number, and password.</param>
		/// <returns>Redirects to the Login page if successful; otherwise, reloads the Create Profile view.</returns>

		[HttpPost]
        public async Task<ActionResult> CreateProfile(CreateProfileViewModel createProfile)
        {
            var applicationUser = new ApplicationUser //creating a new instance of ApplicationUser from registration form + model
            {
                UserName = createProfile.Username,
                FirstName = createProfile.FirstName,
                LastName = createProfile.LastName,
                Email = createProfile.Email,
                PhoneNumber = createProfile.PhoneNr,
            };
            var applicationResult = await _userManager.CreateAsync(applicationUser, createProfile.Password); //creating a new user in the database
            
            if(applicationResult.Succeeded) //assigning user role here, after creation
            {
              var applicationIdentityResult = await _userManager.AddToRoleAsync(applicationUser, "Customer"); //assigning user role, "User" is in the appdbcontext
                
                if(applicationIdentityResult.Succeeded)
                {
                    return RedirectToAction("Login");
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

		/// <summary>
		/// Authenticates a user with the provided username and password and redirects to the ProfilePage if successful.
		/// </summary>
		/// <param name="loginViewModel">The view model containing the user's login credentials, including username and password.</param>
		/// <returns>Redirects to the ProfilePage upon successful login; otherwise, reloads the Login view.</returns>

		[HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);
            if(signInResult != null && signInResult.Succeeded)
            {
                return RedirectToAction("ProfilePage");
            }
            return View();
        }

		/// <summary>
		/// Retrieves and displays the profile details of the currently logged-in user. 
		/// If no user is logged in, redirects to the Login page.
		/// </summary>
		/// <returns>Renders the ProfilePage view with user details if authenticated; otherwise, redirects to the Login page.</returns>

		[HttpGet]
        public async Task<IActionResult> ProfilePage()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if(currentUser != null)
            {
                var profilePageView = new ProfilePageViewModel
                {
                    FirstName = currentUser.FirstName,
                    LastName = currentUser.LastName,
                    Email = currentUser.Email
                };
                return View(profilePageView);
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home"); //redirecting to index in the home controller
        }


        [HttpGet]

		//no need for logic statement since user is redirected to /AccessDenied if not authorized by the Authorize attribute
		public IActionResult AccessDenied() 
		{

			return View();
        }
     
       
    }
}
