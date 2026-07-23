using CleanArquitecture.Domain.Entities.Customer;
using CleanArquitecture.Infraestructure.Context;

namespace CleanArquitecture.Infraestructure.Repositories;

internal sealed class CustomerRepository : Repository<Customer, CustomerId>, ICustomerRepository
{
    public CustomerRepository(AppDbContext dbContext) : base(dbContext)
    {
        
    }
}
