using Microsoft.EntityFrameworkCore;
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
            var car = new Car();

            car.Model = GetCarModel(modelId);
            car.RegistrationNumber = registrationNumber;
            car.FuelCapacity = fuelCapacity;
            car.NumberOfSeats = numberOfSeats;
            car.Price = price;
            car.Mileage = mileage;
            car.EngineType = _dbContext.EngineTypes!.FirstOrDefault(e => e.Id.Equals(engineTypeId));

            if (embeddedDevicesIds != null)
            {
                car.EmbeddedDevices = new List<EmbeddedDevice>();

                foreach (var id in embeddedDevicesIds)
                {
                    var embeddedDevice = _dbContext.EmbeddedDevices!.FirstOrDefault(d => d.Id.Equals(id));
                    if (embeddedDevice != null)
                    {
                        car.EmbeddedDevices.Add(embeddedDevice);
                    }
                }
            }
            try
            {
                _dbContext.Cars!.Add(car);
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
            Model model = new Model();
            model.Name = name;
            model.Brand = _dbContext.Brands!.FirstOrDefault(b => b.Id.Equals(carBrandId));
            try
            {
                _dbContext.Models!.Add(model);
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

        public void DeleteCar(int carId)
        {
            var car = GetCar(carId);
            try
            {
                if (car != null)
                {
                    _dbContext.Cars!.Remove(car);
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
            var model = GetCarModel(carModelId);
            try
            {
                if (model != null)
                {
                    _dbContext.Models!.Remove(model);
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

        public Car? GetCar(int carId)
        {
            return _dbContext.Cars!.FirstOrDefault(c => c.Id.Equals(carId));
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
            return _dbContext.Models!.Where(m => m.Id.Equals(carModelId)).Include(m => m.Brand).FirstOrDefault();
        }

        public List<Model> GetCarModels()
        {
            return _dbContext.Models!.Include(m => m.Brand).ToList();
        }

        public List<Model> GetCarModels(int brandId)
        {
            return _dbContext.Models!.Where(m => m.Brand!.Id.Equals(brandId)).Include(m => m.Brand).ToList();
        }

        public List<Car> GetCars()
        {
            return _dbContext.Cars!.Include(c => c.EmbeddedDevices).Include(c => c.Model).ThenInclude(m => m!.Brand).ToList();
        }

        public List<EmbeddedDevice> GetEmbeddedDevices()
        {
            return _dbContext.EmbeddedDevices!.ToList();
        }

        public List<EmbeddedDevice> GetEmbeddedDevices(string searchWord)
        {
            return _dbContext.EmbeddedDevices!.Where(d => d.Name!.Contains(searchWord)).ToList();
        }

        public List<EngineType> GetEngineTypes()
        {
            return _dbContext.EngineTypes!.ToList();
        }

        public List<EngineType> GetEngineTypes(string searchWord)
        {
            return _dbContext.EngineTypes!.Where(t => t.Name!.Contains(searchWord)).ToList();
        }

        public void UpdateCar(int carId, int? modelId, string? registrationNumber, int? fuelCapacity, int? numberOfSeats, int? price, int? mileage, int? engineTypeId, List<int>? embeddedDevicesIds)
        {
            var car = _dbContext.Cars!.FirstOrDefault(c => c.Id.Equals(carId));

            if (car != null)
            {
                try
                {
                    if (modelId != null)
                    {
                        car.Model = GetCarModel((int)modelId);
                    }
                    if (registrationNumber != null)
                    {
                        car.RegistrationNumber = registrationNumber;
                    }
                    if (fuelCapacity != null)
                    {
                        car.FuelCapacity = (int)fuelCapacity;
                    }
                    if (numberOfSeats != null)
                    {
                        car.NumberOfSeats = (int)numberOfSeats;
                    }
                    if (price != null)
                    {
                        car.Price = (int)price;
                    }
                    if (mileage != null)
                    {
                        car.Mileage = (int)mileage;
                    }
                    if (engineTypeId != null)
                    {
                        car.EngineType = _dbContext.EngineTypes!.FirstOrDefault(e => e.Id.Equals(engineTypeId));
                    }
                    if (embeddedDevicesIds != null)
                    {
                        if (car.EmbeddedDevices != null)
                        {
                            car.EmbeddedDevices.Clear();
                        }
                        else
                        {
                            car.EmbeddedDevices = new List<EmbeddedDevice>();
                        }
                        foreach (var id in embeddedDevicesIds)
                        {
                            var embeddedDevice = _dbContext.EmbeddedDevices!.FirstOrDefault(d => d.Id.Equals(id));
                            if (embeddedDevice != null)
                            {
                                car.EmbeddedDevices.Add(embeddedDevice);
                            }
                        }
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
            var model = GetCarModel(carModelId);
            try
            {
                if (model != null)
                {
                    if (name != null)
                    {
                        model.Name = name;
                    }
                    if (carBrandId != null)
                    {
                        model.Brand = GetCarBrand((int)carBrandId);
                    }
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
    }
}
