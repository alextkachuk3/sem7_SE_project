using sem7_SE_project.Models;

namespace sem7_SE_project.Services.OrderService
{
    public interface IOrderService
    {
        List<OrderStatus> GetOrderStatuses();

        List<Order> GetOrders();

        Order? GetOrder(int orderId);

        void AddOrder(int clientId, int carId, bool testDriveNeeded, int orderStatusId);

        void EditOrder(int orderId, int orderStatusId);

        void DeleteOrder(int orderId);

    }
}
