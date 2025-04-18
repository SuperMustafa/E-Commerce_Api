

using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.Dtos.Basket;

namespace Presentaion.Controllers.Baskets
{
  
    public class BasketController(IServiceManager serviceManager) : ApiController
    {
        [HttpGet("{id}")] //get : baseurl/api/basket/id

        public async Task<ActionResult<BasketDto>> Get(string id)
        {
            var basket = await serviceManager.basketService.GetBasketAsync(id);
            return Ok(basket);
        }

        [HttpPost]  //post : baseurl/api/basket
        public async Task<ActionResult<BasketDto>> Update(BasketDto basketDto)
        {
            var basket = await serviceManager.basketService.UpdateBasketAsync(basketDto);
            return Ok(basket);
        }

        [HttpDelete("{id}")]  //delete : baseurl/api/basket/id
        public async Task<ActionResult> Delete(string id)
        {
             await serviceManager.basketService.DeleteBasketAsync(id);
            return NoContent(); // 204
        }
    }

}
