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

        public void DeleteOrder(int orderId)
        {
            Order? order = _dbContext.Orders!.FirstOrDefault(o => o.Id.Equals(orderId));
            if(order != null)
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
