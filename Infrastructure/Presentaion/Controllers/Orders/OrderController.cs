using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.Dtos.Order;

namespace Presentaion.Controllers.Orders
{
    [Authorize]
    public class OrderController(IServiceManager serviceManager):ApiController
    {
        [HttpPost]
        public async Task<ActionResult<OrderResultDto>> CreateOrder(OrderRequestDto orderRequest)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await serviceManager.OrderService.CreateOrderAsync(orderRequest, email);
            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<OrderResultDto>> GetOrderByEmail() 
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Order = await serviceManager.OrderService.GetOrdersByEmailAsync(email);
            return Ok(Order);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResultDto>> GetOrderById(Guid id)
        {
            var order = await serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(order);
        }

        [AllowAnonymous]
        [HttpGet("deliveryMethod")]
        public async Task<ActionResult<DeliveryMethodDto>> GetDeliveryMethod() 
        {
            var DeliveryMethod = await serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(DeliveryMethod);
        }
    }
}
