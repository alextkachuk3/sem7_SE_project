using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using sem7_SE_project.Models;
using System.Security.Claims;
using sem7_SE_project.Services.UserService;
using sem7_SE_project.Services.CarService;
using Microsoft.AspNetCore.Authorization;
using sem7_SE_project.Services.ClientService;
using System.ComponentModel.DataAnnotations;

namespace sem7_SE_project.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IUserService _userService;
        private readonly ICarService _carService;
        private readonly IClientService _clientService;

        public AdminController(ILogger<AdminController> logger, IUserService userService, ICarService carService, IClientService clientService)
        {
            _logger = logger;
            _userService = userService;
            _carService = carService;
            _clientService = clientService;
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

        [Authorize(Roles = "admin")]
        public IActionResult EditCar(int carId)
        {
            Car? car = _carService.GetCar(carId);
            if (car == null)
            {
                return Redirect("~/admin/cars/");
            }
            return View(car);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult EditCar(int carId, int carModelId, string registrationNumber, int fuelCapacity, int numberOfSeats, int price, int mileage, int engineTypeId, List<int>? embeddedDevicesIds)
        {
            _carService.UpdateCar(carId, carModelId, registrationNumber, fuelCapacity, numberOfSeats, price, mileage, engineTypeId, embeddedDevicesIds);
            return Redirect("~/admin/cars/");
        }

        [Authorize(Roles = "admin")]
        public IActionResult Clients()
        {
            var clients = _clientService.GetClients();
            return View(clients);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Clients(List<int> clientsIds)
        {
            foreach (var id in clientsIds)
            {
                _clientService.DeleteClient(id);
            }
            return Redirect("~/admin/clients/");
        }

        [Authorize(Roles = "admin")]
        public IActionResult AddClient()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddClient(string firstName, string lastName, string? address, string? phoneNumber, string? email)
        {
            _clientService.AddClient(firstName, lastName, address, phoneNumber, email);
            return Redirect("~/admin/clients/");
        }

        [Authorize(Roles = "admin")]
        public IActionResult EditClient(int clientId)
        {
            Client? client = _clientService.GetClient(clientId);
            if (client == null)
            {
                return Redirect("~/admin/clients/");
            }
            return View(client);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult EditClient(int clientId, string firstName, string lastName, string? address, string? phoneNumber, string? email)
        {
            _clientService.EditClient(clientId, firstName, lastName, address, phoneNumber, email);
            return Redirect("~/admin/clients/");
        }

    }
}
