using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using sem7_SE_project.Models;
using System.Security.Claims;
using sem7_SE_project.Services.UserService;

namespace sem7_SE_project.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IUserService _userService;

        public AdminController(ILogger<AdminController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            if(HttpContext.User!.Identity!.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return LocalRedirect("~/admin/login");
            }           
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            User? user = _userService.GetUser(username);

            if (user == null)
            {
                ViewBag.AuthError = "User with this username not exists!";
                return View();
            }
            else if (user.CheckPassword(password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "admin")
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return LocalRedirect("~/admin");
            }
            else
            {
                ViewBag.AuthError = "Wrong password!";
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return LocalRedirect("~/");
        }
    }
}
