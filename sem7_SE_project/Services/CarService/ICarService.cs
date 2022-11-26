using sem7_SE_project.Models;
using System.ComponentModel.DataAnnotations;

namespace sem7_SE_project.Services.CarService
{
    public interface ICarService
    {
        public List<EmbeddedDevice> GetEmbeddedDevices();

        public List<EmbeddedDevice> GetEmbeddedDevices(string searchWord);

        public List<EngineType> GetEngineTypes();

        public List<EngineType> GetEngineTypes(string searchWord);

        public List<Brand> GetCarBrands();

        public Brand? GetCarBrand(int carBrandId);

        public void DeleteCarBrand(int carBrandId);

        public void AddCarBrand(string name);

        public void UpdateCarBrand(int carBrandId, string? name);

        public List<Model> GetCarModels();

        public List<Model> GetCarModels(int brandId);

        public Model? GetCarModel(int carModelId);

        public void AddCarModel(int carBrandId, string name);

        public void UpdateCarModel(int carModelId, string? name, int? carBrandId);

        public void DeleteCarModel(int carModelId);

        public List<Car> GetCars();

        public Car? GetCar(int carId);

        public void AddCar(int modelId, string registrationNumber, int fuelCapacity, int numberOfSeats, int price, int mileage, int engineTypeId, List<int>? embeddedDevicesIds);

        public void DeleteCar(int carId);

        public void UpdateCar(int carId, int? modelId, string? registrationNumber, int? fuelCapacity, int? numberOfSeats, int? price, int? mileage, int? engineTypeId, List<int>? embeddedDevicesIds);

    }
}
