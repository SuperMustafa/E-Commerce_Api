using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Services.Abstraction;

namespace Services.Cash
{
    public class CashService(ICashRepository cashRepository) : ICashService
    {
        public async Task<string> GetCashedValueAsync(string key)
        {
            var result = await cashRepository.GetAsync(key);
            return result;
        }

        public async Task setCashValueAsync(string key, object value, TimeSpan duration)
        {
            await cashRepository.SetAsync(key, value, duration);

        }
    }
}
