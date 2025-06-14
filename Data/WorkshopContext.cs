using AutoWorkshopWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshopWeb.Data;

public class WorkshopContext : DbContext
{
    public WorkshopContext(DbContextOptions<WorkshopContext> options)
        : base(options)
    {
    }

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Car> Cars => Set<Car>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<ServiceLog> ServiceLogs => Set<ServiceLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>()
            .HasIndex(c => c.LicensePlate)
            .IsUnique();

        modelBuilder.Entity<ServiceLog>()
            .HasOne(sl => sl.Service)
            .WithMany(s => s.Logs)
            .HasForeignKey(sl => sl.ServiceId)
            .OnDelete(DeleteBehavior.SetNull);

        base.OnModelCreating(modelBuilder);
    }
}