﻿

namespace Persistance.Data.Configurations
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(P => P.ProductBrand)
                  .WithMany()
                  .HasForeignKey(P => P.ProductBrandId);

            builder.HasOne(P => P.ProductType)
                .WithMany()
                .HasForeignKey(P => P.TypeId);
            builder.Property(P => P.Price).HasColumnType("decimal(18,3)");
        }
    }
}
