using Microsoft.AspNetCore.Mvc;
using sem7_SE_project.Services.OrderService;

namespace sem7_SE_project.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public JsonResult GetOrderStatuses()
        {
            return new JsonResult(orderService.GetOrderStatuses());
        }
    }
}
