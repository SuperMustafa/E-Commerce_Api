
namespace Shared.Dtos.Basket
{
    public record BasketDto
    {
        public string Id { get; init; }
        public IEnumerable<BasketItemDto> BasketItems { get; set; }
    }
}
