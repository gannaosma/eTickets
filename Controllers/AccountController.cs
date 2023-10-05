using eTickets.Data;
using eTickets.Data.Static;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;

        }

        public async Task<IActionResult> Users()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        public IActionResult Login()
        {
            var respone = new LoginVM();
            return View(respone);
        }

        [HttpPost]
		public async Task<IActionResult> Login(LoginVM loginVM)
		{
            if (!ModelState.IsValid)
                return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAdress);

            if(user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);

                if(passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movies");
                    }
                }
				TempData["Error"] = "Wrong Credentials, Please tryy again!";

				return View(loginVM);
			}

            TempData["Error"] = "Wrong Credentials, Please tryy again!";

			return View(loginVM);
		}

		public IActionResult Register()
		{
			var respone = new RegisterVM();
			return View(respone);
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterVM registerVM)
		{
			if (!ModelState.IsValid)
				return View(registerVM);

			var user = await _userManager.FindByEmailAsync(registerVM.EmailAdress);

			if (user != null)
			{
				TempData["Error"] = "This Email is already used";

				return View(registerVM);
			}

            var newUser = new ApplicationUser()
            {
                FullName = registerVM.FullName,
                Email = registerVM.EmailAdress,
                UserName = registerVM.EmailAdress,
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if(!newUserResponse.Succeeded)
            {
				TempData["Error"] = newUserResponse;

				return View(registerVM);
				
			}
			await _userManager.AddToRoleAsync(newUser, UserRoles.User);
			return View("RegisterCompleted");



		}

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");
        }
	}
}
