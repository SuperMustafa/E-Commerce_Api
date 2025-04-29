using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.OrderEntites;

namespace Persistance.Data.Configurations
{
    internal class OrderConfigurations : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.OwnsOne(O => O.ShippingAddress, O => O.WithOwner());
            builder.HasMany(O => O.OrderItems).WithOne();
            builder.Property(O=>O.PaymentStatus).HasConversion(P=>P.ToString(),P=>Enum.Parse<OrderPaymentStatus>(P));
            builder.HasOne(O=>O.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);
            builder.Property(O=>O.SubTotal).HasColumnType("decimal(18,3)");
            builder.HasMany(O=>O.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
