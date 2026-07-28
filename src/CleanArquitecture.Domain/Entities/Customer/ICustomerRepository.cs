using CleanArquitecture.Domain.Entities.Customer.ValueObjects;
using CleanArquitecture.Domain.Entities.Shared.ValueObjects;
using CleanArquitecture.Domain.Shared.Repositories;

namespace CleanArquitecture.Domain.Entities.Customer;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> GetCustomerByEmail(Email email, CancellationToken cancellationToken = default);
    Task<Customer?> GetCustomerByCpf(Cpf cpf, CancellationToken cancellationToken = default);
}
