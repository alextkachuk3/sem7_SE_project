using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using sem7_SE_project.Models;
using System.Security.Claims;
using sem7_SE_project.Services.UserService;
using sem7_SE_project.Services.CarService;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace sem7_SE_project.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IUserService _userService;
        private readonly ICarService _carService;

        public AdminController(ILogger<AdminController> logger, IUserService userService, ICarService carService)
        {
            _logger = logger;
            _userService = userService;
            _carService = carService;
        }

        public IActionResult Index()
        {
            if (HttpContext.User!.Identity!.IsAuthenticated)
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
            return Redirect("~/");
        }

        [Authorize(Roles = "admin")]
        public IActionResult CarBrands()
        {
            var brands = _carService.GetCarBrands();
            return View(brands);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult CarBrands(List<int> carBrandsIds)
        {
            foreach (var id in carBrandsIds)
            {
                _carService.DeleteCarBrand(id);
            }
            return Redirect("~/admin/carbrands/");
        }

        [Authorize(Roles = "admin")]
        public IActionResult AddCarBrand()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddCarBrand(string carBrandName)
        {
            _carService.AddCarBrand(carBrandName);
            return Redirect("~/admin/carbrands/");
        }

        [Authorize(Roles = "admin")]
        public IActionResult EditCarBrand(int carBrandId)
        {
            var brand = _carService.GetCarBrand(carBrandId);
            return View(brand);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult EditCarBrand(int carBrandId, string carBrandName)
        {
            _carService.UpdateCarBrand(carBrandId, carBrandName);
            return Redirect("~/admin/carbrands/");
        }

        [Authorize(Roles = "admin")]
        public IActionResult CarModels()
        {
            var carModels = _carService.GetCarModels();
            return View(carModels);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult CarModels(List<int> carModelsIds)
        {
            foreach (var id in carModelsIds)
            {
                _carService.DeleteCarModel(id);
            }
            return Redirect("~/admin/carmodels/");
        }

        [Authorize(Roles = "admin")]
        public IActionResult AddCarModel()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddCarModel(string carModelName, int carBrandId)
        {
            _carService.AddCarModel(carBrandId, carModelName);
            return Redirect("~/admin/carmodels/");
        }

        [Authorize(Roles = "admin")]
        public IActionResult EditCarModel(int carModelId)
        {
            var carModel = _carService.GetCarModel(carModelId);
            return View(carModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult EditCarModel(int carModelId, string carModelName, int carBrandId)
        {
            _carService.UpdateCarModel(carModelId, carModelName, carBrandId);
            return Redirect("~/admin/carmodels/");
        }

        [Authorize(Roles = "admin")]
        public IActionResult Cars()
        {
            var cars = _carService.GetCars();
            return View(cars);
        }

        [Authorize(Roles = "admin")]
        public IActionResult AddCar()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddCar(int carModelId, string registrationNumber, int fuelCapacity, int numberOfSeats, int price, int mileage, int engineTypeId, List<int>? embeddedDevicesIds)
        {
            _carService.AddCar(carModelId, registrationNumber, fuelCapacity, numberOfSeats, price, mileage, engineTypeId, embeddedDevicesIds);
            return Redirect("~/admin/cars/");
        }


    }
}
