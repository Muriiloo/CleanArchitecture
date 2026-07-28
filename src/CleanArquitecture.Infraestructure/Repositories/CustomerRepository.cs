using CleanArquitecture.Domain.Entities.Customer;
using CleanArquitecture.Domain.Entities.Customer.ValueObjects;
using CleanArquitecture.Domain.Entities.Shared.ValueObjects;
using CleanArquitecture.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArquitecture.Infraestructure.Repositories;

internal sealed class CustomerRepository : Repository<Customer, CustomerId>, ICustomerRepository
{
    private readonly AppDbContext _dbContext;
    public CustomerRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Customer?> GetCustomerByCpf(Cpf cpf, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Customer>().FirstOrDefaultAsync(x => x.Cpf == cpf, cancellationToken);
    }

    public async Task<Customer?> GetCustomerByEmail(Email email, CancellationToken cancellationToken = default)
    {
       return await _dbContext.Set<Customer>().FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }
}
