using Microsoft.AspNetCore.Mvc;
using sem7_SE_project.Services.CarService;

namespace sem7_SE_project.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;

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
    }
}
