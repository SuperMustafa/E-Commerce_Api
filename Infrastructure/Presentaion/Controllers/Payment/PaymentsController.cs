using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.Dtos.Basket;

namespace Presentaion.Controllers.Payment
{

    public class PaymentsController(IServiceManager serviceManager ):ApiController
    {
        [HttpPost("{BasketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentAsync(string basketId)
        {
            var result = await serviceManager.paymentService.CreateOrUpdatePaymentAsync(basketId);
            return Ok(result);
        }


        [HttpPost("webhook")]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            await serviceManager.paymentService.UpdateOrderPaymentStatus(json, Request.Headers["Stripe-Signature"]);
            return new EmptyResult();
        }
    }
}
