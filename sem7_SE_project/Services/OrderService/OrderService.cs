using Microsoft.EntityFrameworkCore;
using sem7_SE_project.Data;
using sem7_SE_project.Models;

namespace sem7_SE_project.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddOrder(int clientId, int carId, bool testDriveNeeded, int orderStatusId)
        {
            Order order = new Order();
            order.Client = _dbContext.Clients!.FirstOrDefault(c => c.Id.Equals(clientId));
            order.Car = _dbContext.Cars!.FirstOrDefault(c => c.Id.Equals(carId));
            order.IsTestDriveNeeded = testDriveNeeded;
            order.OrderStatus = _dbContext.OrderStatuses!.FirstOrDefault(s => s.Id.Equals(orderStatusId));
            order.CreationDateTime = DateTime.Now;

            try
            {
                _dbContext.Orders!.Add(order);
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

        public void DeleteOrder(int orderId)
        {
            Order? order = _dbContext.Orders!.FirstOrDefault(o => o.Id.Equals(orderId));
            if (order != null)
            {
                try
                {
                    _dbContext.Orders!.Remove(order);
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

        public void EditOrder(int orderId, int orderStatusId)
        {
            Order? order = GetOrder(orderId);

            if (order != null)
            {
                try
                {
                    order.OrderStatus = _dbContext.OrderStatuses!.FirstOrDefault(s => s.Id.Equals(orderStatusId));
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

        public Order? GetOrder(int orderId)
        {
            return _dbContext.Orders!.Where(o => o.Id.Equals(orderId)).Include(o => o.Car).ThenInclude(c => c!.Model).ThenInclude(m => m!.Brand).Include(o => o.Client).Include(o => o.OrderStatus).FirstOrDefault();
        }

        public List<Order> GetOrders()
        {
            return _dbContext.Orders!.Include(o => o.Car).ThenInclude(c => c!.Model).ThenInclude(m => m!.Brand).Include(o => o.Client).Include(o => o.OrderStatus).ToList();
        }

        public List<OrderStatus> GetOrderStatuses()
        {
            return _dbContext.OrderStatuses!.ToList();
        }
    }
}
