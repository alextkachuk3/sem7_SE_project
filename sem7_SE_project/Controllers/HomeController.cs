using Microsoft.AspNetCore.Mvc;
using sem7_SE_project.Models;
using sem7_SE_project.Services.CarService;

namespace sem7_SE_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICarService _carService;

        public HomeController(ILogger<HomeController> logger, ICarService carService)
        {
            _logger = logger;
            _carService = carService;
        }

        public IActionResult Index(int? page, int? minPrice, int? maxPrice, int? brandId, int? engineTypeId, int? minFuelCapacity, int? maxFuelCapacity, List<int>? embeddedDevicesIds)
        {
            Tuple<List<Car>?, int> carsTuple = _carService.SearchCars(page, minPrice, maxPrice, brandId, engineTypeId, minFuelCapacity, maxFuelCapacity, embeddedDevicesIds);
            return View(carsTuple);
        }
    }
}