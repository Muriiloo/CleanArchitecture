using CleanArquitecture.Domain.Abstrations;
using CleanArquitecture.Domain.Entities.Customer.Events;
using CleanArquitecture.Domain.Entities.Customer.ValueObjects;

namespace CleanArquitecture.Domain.Entities.Customer;

public class Customer : Entity<CustomerId>
{
    private Customer(CustomerId id, FullName fullName, Password password, Cpf cpf, BirthDay birthDay, Email email) : base(id)
    {
        FullName = fullName;
        Password = password;
        Cpf = cpf;
        BirthDay = birthDay;
        Email = email;
    }

    public FullName FullName { get; private set; }
    public Password Password { get; private set; }
    public Cpf Cpf { get; private set; }
    public BirthDay BirthDay { get; private set; }
    public Email Email { get; private set; }

    public static Result<Customer> Create(FullName fullName, Password password, Email email, Cpf cpf, BirthDay birthDay)
    {
        var customer = new Customer(CustomerId.New(), fullName, password, cpf, birthDay, email);
        customer.RaiseDomainEvent(new CustomerCreatedDomainEvent(customer.Id));

        return Result.Success(customer);
    }
}
