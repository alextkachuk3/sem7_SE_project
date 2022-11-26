using Microsoft.AspNetCore.Mvc;
using sem7_SE_project.Models;
using sem7_SE_project.Services.CarService;

namespace sem7_SE_project.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public IActionResult Index(int carId)
        {
            var car = _carService.GetCar(carId);
            return View(car);
        }

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public JsonResult GetCarBrands()
        {
            return new JsonResult(_carService.GetCarBrands());
        }

        [HttpGet]
        public JsonResult GetCarModels()
        {
            return new JsonResult(_carService.GetCarModels());
        }

        [HttpPost]
        public JsonResult GetCarModels(int brandId)
        {
            return new JsonResult(_carService.GetCarModels(brandId));
        }

        [HttpGet]
        public JsonResult GetEngineTypes()
        {
            return new JsonResult(_carService.GetEngineTypes());
        }

        [HttpPost]
        public JsonResult GetEngineTypes(string searchWord)
        {
            return new JsonResult(_carService.GetEngineTypes(searchWord));
        }

        [HttpPost]
        public JsonResult GetEmbeddedDevices(string? searchWord)
        {
            List<EmbeddedDevice> embeddedDevices;
            if (searchWord == null)
            {
                embeddedDevices = _carService.GetEmbeddedDevices();
            }
            else
            {
                embeddedDevices = _carService.GetEmbeddedDevices(searchWord);
            }
            return new JsonResult(embeddedDevices.Select(s => new
            {
                id = s.Id,
                text = s.Name
            }));
        }
    }
}
