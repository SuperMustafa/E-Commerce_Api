using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StackExchange.Redis;

namespace Persistance.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connectionMultiplexer) : IBasketRepository
    {
        private readonly IDatabase _database = connectionMultiplexer.GetDatabase();
        public async Task<bool> DeleteBasketAsync(string id)
        {
          return await _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var basketData = await _database.StringGetAsync(id);

            if (basketData.IsNullOrEmpty)
                return null;

            try
            {
                return JsonSerializer.Deserialize<CustomerBasket>(basketData);
            }
            catch (JsonException ex)
            {
                // Optional: log the exception here
                // _logger.LogError(ex, "Failed to deserialize basket for id: {Id}", id);
                return null;
            }
        }
        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null) // put data as a json 
        {
           var JsonBasket = JsonSerializer.Serialize(basket);
            var IsCreatedOrUpdated = await _database.StringSetAsync(basket.Id, JsonBasket, TimeToLive ?? TimeSpan.FromDays(30));
            return IsCreatedOrUpdated ? await GetBasketAsync(basket.Id) : null; 
        }
    }
}
