
namespace Shared.Dtos.Basket
{
    public record BasketDto
    {
        public string Id { get; init; }
        public IEnumerable<BasketItemDto> BasketItems { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int DeliveryMethodId { get; set; }
        public decimal ShipingPrice { get; set; }
    }
}
