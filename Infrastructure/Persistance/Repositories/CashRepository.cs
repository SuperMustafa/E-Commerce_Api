using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Persistance.Repositories
{
    public class CashRepository(IConnectionMultiplexer connectionMultiplexer) :ICashRepository
    {
        private readonly IDatabase database = connectionMultiplexer.GetDatabase();

        public async Task<string?> GetAsync(string? key)
        {
            var value = await database.StringGetAsync(key);
            return value.IsNullOrEmpty ? value : default;
        }

        public async Task SetAsync(string key, object value, TimeSpan duration)
        {
            var serlizeObject = JsonSerializer.Serialize(value);
            await database.StringSetAsync(key, serlizeObject, duration);
        }
    }
}
