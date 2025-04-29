using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.OrderEntites;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Services.Abstraction.Payment;
using Services.Specifications;
using Shared.Dtos.Basket;
using Stripe;
using Stripe.V2;
using Product=Domain.Entities.Product;


namespace Services.Payment
{
    internal class PaymentService(IBasketRepository basketRepository,IUnitOfWork unitOfWork,IMapper mapper,IConfiguration configuration ) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentAsync(string basketId)
        {
            //stripe Configurations
            StripeConfiguration.ApiKey = configuration.GetRequiredSection("StripeSettings")["SecretKey"];

            //get basket
            var basket = await basketRepository.GetBasketAsync(basketId)??
                throw new BasketNotFoundException(basketId);

            //update price inside basket

            foreach(var item in basket.BasketItems)
            {
                var product = await unitOfWork.GetRepository<Product, int>().GetAsync(item.Id)??
                    throw new ProductNotfoundException(item.Id);
                item.Price= product.Price;
            }

            // check deliveryMethod
            if (!basket.DeliveryMethodId.HasValue) throw new Exception("No Delivery Method Is Selected");
            var Method = await unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetAsync(basket.DeliveryMethodId.Value)
                ?? throw new DeliveryMethodException(basket.DeliveryMethodId.Value);
            basket.ShipingPrice = Method.Price;


            //total amount
            var Amount = (long)(basket.BasketItems.Sum(BI => BI.Quantity * BI.Price) + basket.ShipingPrice)*100;

            //create or update payment intent

            if (string.IsNullOrWhiteSpace(basket.PaymentIntentId))
            {
                var CreateOptions = new PaymentIntentCreateOptions
                {
                    Amount = Amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" },
                };

                var PaymentIntent = await new PaymentIntentService().CreateAsync(CreateOptions);
                basket.PaymentIntentId = PaymentIntent.Id;
                basket.ClientSecret = PaymentIntent.ClientSecret;
            }
            // Update
            else
            {
                var UpdatedOptions = new PaymentIntentUpdateOptions
                {
                    Amount = Amount
                };
                await new PaymentIntentService().UpdateAsync(basket.PaymentIntentId, UpdatedOptions);
            }

            //update basket at data base
            await basketRepository.UpdateBasketAsync(basket);

            // return basketDto

            return mapper.Map<BasketDto>(basket);
            }

        public async Task UpdateOrderPaymentStatus(string request, string stripeHeader)
        {
            // WebHook Secret
            var endPointSecret = configuration.GetRequiredSection("StripeSettings")["EndPointSecret"];
            // All Informations About Event That Was Sent By Stripe

            var stripeEvent = EventUtility.ConstructEvent(request, stripeHeader, endPointSecret);

            // That Mean Payment Con

            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

            switch (stripeEvent.Type)
            {
                case "payment_intent.payment_failed":
                    await UpdatePaymentFailed(paymentIntent!.Id);
                    break;

                case "payment_intent.succeeded":
                    await UpdatePaymentReceived(paymentIntent!.Id);
                    break;

                // ... handle other event types
                default:
                    Console.WriteLine("Unhandeled event type:{0}",stripeEvent.Type);
                    break;
            }

        }

        private async Task UpdatePaymentReceived(string paymentIntentId)
        {
            var order = await unitOfWork.GetRepository<OrderEntity, Guid>()
                .GetAsync(new OrderWithPaymentintentSpecifications(paymentIntentId))
                ?? throw new Exception();
            // Change PaymentStatus  
            order.PaymentStatus = OrderPaymentStatus.PaymentRecevied;
            unitOfWork.GetRepository<OrderEntity, Guid>().UpdateAsync(order);
            await unitOfWork.SaveChangesAsync();
        }

        private async Task UpdatePaymentFailed(string paymentIntentId)
        {
            var order = await unitOfWork.GetRepository<OrderEntity, Guid>()
                .GetAsync(new OrderWithPaymentintentSpecifications(paymentIntentId))
                ?? throw new Exception();
            // Change PaymentStatus  
            order.PaymentStatus = OrderPaymentStatus.PaymentFailed;
            unitOfWork.GetRepository<OrderEntity, Guid>().UpdateAsync(order);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
