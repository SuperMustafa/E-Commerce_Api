using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Order
{
    public record OrderRequestDto
    {
        public string BasketId { get; init; }
        public ShipingAddressDto ShipingAddress { get; init; }
        public int DekiveryMethodId { get; init; }
    }
}
