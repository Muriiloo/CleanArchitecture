using CleanArquitecture.Application.Authentication;
using CleanArquitecture.Domain.Abstrations;
using CleanArquitecture.Domain.Entities.Customer;
using CleanArquitecture.Domain.Entities.Event;
using CleanArquitecture.Domain.Entities.Producer;
using CleanArquitecture.Infraestructure.Authentication;
using CleanArquitecture.Infraestructure.Context;
using CleanArquitecture.Infraestructure.Repositories;
using CleanArquitecture.Infraestructure.Time;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArquitecture.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<AppDbContext>());

        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IProducerRepository, ProducerRepository>();
        services.AddScoped<IEventRepository, EventRepository>();

        return services;
    }
}
