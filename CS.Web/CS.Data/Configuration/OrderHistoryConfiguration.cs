using CS.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CS.Data.Configuration;

public class OrderHistoryConfiguration:IEntityTypeConfiguration<OrderHistory>
{
    public void Configure(EntityTypeBuilder<OrderHistory> builder)
    {
        builder.HasKey(h=>new {h.OrderId,h.OrderDate} );
        builder.HasOne(h => h.Order)
            .WithMany(o => o.OrderHistories)
            .HasForeignKey(h => h.OrderId);
    }
}