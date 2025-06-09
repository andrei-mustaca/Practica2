using CS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CS.Data;

public class DataContext:DbContext
{
    public DataContext(DbContextOptions<DataContext> options):base(options)
    {
        
    }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Courier> Couriers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderAcceptance> OrderAcceptances { get; set; }
    public DbSet<OrderHistory> OrderHistories { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Route> Routes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}