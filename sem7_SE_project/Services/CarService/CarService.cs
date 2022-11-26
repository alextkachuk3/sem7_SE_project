using sem7_SE_project.Data;
using sem7_SE_project.Models;
using System.Xml.Linq;

namespace sem7_SE_project.Services.CarService
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _dbContext;

        public CarService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddCar(int modelId, string registrationNumber, int fuelCapacity, int numberOfSeats, int price, int mileage, int engineTypeId, List<int>? embeddedDevicesIds)
        {
            throw new NotImplementedException();
        }

        public void AddCarBrand(string name)
        {
            Brand brand = new Brand();
            brand.Name = name;
            try
            {
                _dbContext.Brands!.Add(brand);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                _dbContext.SaveChanges();
            }
            
        }

        public void AddCarModel(int carBrandId, string name)
        {
            throw new NotImplementedException();
        }

        public void DeleteCar(int carId)
        {
            throw new NotImplementedException();
        }

        public void DeleteCarBrand(int carBrandId)
        {
            var brand = GetCarBrand(carBrandId);
            try
            {
                if (brand != null)
                {
                    _dbContext.Brands!.Remove(brand);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                _dbContext.SaveChanges();
            }
        }

        public void DeleteCarModel(int carModelId)
        {
            throw new NotImplementedException();
        }

        public Car? GetCar(int carId)
        {
            throw new NotImplementedException();
        }

        public Brand? GetCarBrand(int carBrandId)
        {
            return _dbContext.Brands!.FirstOrDefault(b => b.Id.Equals(carBrandId));
        }

        public List<Brand> GetCarBrands()
        {
            return _dbContext.Brands!.ToList();
        }

        public Model? GetCarModel(int carModelId)
        {
            throw new NotImplementedException();
        }

        public List<Model> GetCarModels()
        {
            throw new NotImplementedException();
        }

        public List<Car> GetCars()
        {
            throw new NotImplementedException();
        }

        public void UpdateCar(int carId, int? modelId, string? registrationNumber, int? fuelCapacity, int? numberOfSeats, int? price, int? mileage, int? engineTypeId, List<int>? embeddedDevicesIds)
        {
            throw new NotImplementedException();
        }

        public void UpdateCarBrand(int carBrandId, string? name)
        {
            var brand = GetCarBrand(carBrandId);
            try
            {
                if (brand != null)
                {
                    brand.Name = name;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                _dbContext.SaveChanges();
            }            
        }

        public void UpdateCarModel(int carModelId, string? name, int? carBrandId)
        {
            throw new NotImplementedException();
        }
    }
}
