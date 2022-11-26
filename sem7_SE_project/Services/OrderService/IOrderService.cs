using sem7_SE_project.Models;

namespace sem7_SE_project.Services.OrderService
{
    public interface IOrderService
    {
        List<OrderStatus> GetOrderStatuses();

        List<Order> GetOrders();

        void AddOrder(int clientId, int carId, bool testDriveNeeded, int orderStatusId);

        void DeleteOrder(int orderId);

    }
}
