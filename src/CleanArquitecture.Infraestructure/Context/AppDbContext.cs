using CleanArquitecture.Domain.Abstrations;
using CleanArquitecture.Domain.Entities.Customer;
using Microsoft.EntityFrameworkCore;

namespace CleanArquitecture.Infraestructure.Context;

public class AppDbContext : DbContext, IUnitOfWork
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Customer> Customers { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
