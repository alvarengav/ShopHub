using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;
using OrderService.Domain.ValueObjects;

namespace OrderService.Infrastructure.Data.Configuration;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.Id);

        builder
            .Property(oi => oi.Id)
            .HasConversion(id => id.Value, value => new OrderItemId(value))
            .ValueGeneratedOnAdd();

        builder
            .Property(oi => oi.ProductId)
            .HasConversion(id => id.Value, value => new ProductId(value));

        builder
            .Property(oi => oi.OrderId)
            .HasConversion(id => id.Value, value => new OrderId(value));
    }
}
