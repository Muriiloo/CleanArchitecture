using CleanArquitecture.Domain.Entities.Customer;
using CleanArquitecture.Domain.Entities.Customer.ValueObjects;
using CleanArquitecture.Domain.Entities.Shared.ValueObjects;
using CleanArquitecture.Domain.Shared.Repositories;

namespace CleanArquitecture.Infraestructure.Repositories.InMemoryRepositories;

public sealed class InMemoryCustomerRepository : IRepository<Customer>, ICustomerRepository
{
    private readonly List<Customer> _customers = [];
    public void Add(Customer entity)
    {
        _customers.Add(entity);
    }

    public Task<Customer?> GetCustomerByCpf(Cpf cpf, CancellationToken cancellationToken = default)
    {
        var customer = _customers.FirstOrDefault(c => c.Cpf == cpf);

        return Task.FromResult(customer);
    }

    public Task<Customer?> GetCustomerByEmail(Email email, CancellationToken cancellationToken = default)
    {
        var customer = _customers.FirstOrDefault(c => c.Email == email);

        return Task.FromResult(customer);
    }
}
