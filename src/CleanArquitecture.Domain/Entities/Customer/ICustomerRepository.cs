using CleanArquitecture.Domain.Entities.Customer.ValueObjects;

namespace CleanArquitecture.Domain.Entities.Customer;

public interface ICustomerRepository
{
    void Add(Customer customer);
    Task<Customer?> GetCustomerByEmail(Email email, CancellationToken cancellationToken = default);
}
