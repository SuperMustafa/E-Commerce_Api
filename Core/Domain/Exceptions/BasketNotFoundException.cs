

namespace Domain.Exceptions
{
    public class BasketNotFoundException:Exception
    {
        public BasketNotFoundException(string id):base($"Basket with Id {id} dosen't exist") { }
    }
}
