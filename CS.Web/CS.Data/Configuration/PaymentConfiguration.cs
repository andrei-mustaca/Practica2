using CS.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CS.Data.Configuration;

public class PaymentConfiguration:IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.OrderId);
        builder.HasOne(p=>p.Order)
            .WithOne(o=>o.Payment)
            .HasForeignKey<Payment>(p=>p.OrderId);
    }
}