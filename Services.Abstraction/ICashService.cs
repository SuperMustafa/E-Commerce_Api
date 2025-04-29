using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface ICashService
    {
        public Task<string> GetCashedValueAsync(string key);
        public Task setCashValueAsync(string key, object value,TimeSpan duration);
    }
}
