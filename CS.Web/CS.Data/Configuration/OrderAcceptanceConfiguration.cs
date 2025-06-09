using CS.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CS.Data.Configuration;

public class OrderAcceptanceConfiguration:IEntityTypeConfiguration<OrderAcceptance>
{
    public void Configure(EntityTypeBuilder<OrderAcceptance> builder)
    {
        builder.HasKey(a => a.OrderId);
        builder.HasOne(a=>a.Order)
            .WithOne(b=>b.OrderAcceptance)
            .HasForeignKey<OrderAcceptance>(a => a.OrderId);
        builder.HasOne(a => a.Courier)
            .WithMany(b => b.OrderAcceptances)
            .HasForeignKey(a => a.CourierId);
    }
}