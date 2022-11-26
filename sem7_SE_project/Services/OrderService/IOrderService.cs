using sem7_SE_project.Models;

namespace sem7_SE_project.Services.OrderService
{
    public interface IOrderService
    {
        List<OrderStatus> GetOrderStatuses();

        List<Order> GetOrders();

        void DeleteOrder(int orderId);

    }
}
