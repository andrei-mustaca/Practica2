using CS.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CS.Data.Configuration;

public class OrderConfiguration:IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.HasOne(o => o.Client)
            .WithMany(o => o.Orders)
            .HasForeignKey(o => o.ClientId);
        builder.HasOne(o => o.DepartureRoute)
            .WithMany(o=>o.DepartureOrders)
            .HasForeignKey(o => o.DepartureId);
        builder.HasOne(o => o.DestinationRoute)
            .WithMany(o=>o.DestinationOrders)
            .HasForeignKey(o=>o.DestinationId);
    }
}