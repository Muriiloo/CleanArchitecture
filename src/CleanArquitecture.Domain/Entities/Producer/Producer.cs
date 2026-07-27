using CleanArquitecture.Domain.Abstrations;
using CleanArquitecture.Domain.Entities.Producer.Events;
using CleanArquitecture.Domain.Entities.Producer.ValueObjects;
using CleanArquitecture.Domain.Entities.Shared.ValueObjects;

namespace CleanArquitecture.Domain.Entities.Producer;

public sealed class Producer : Entity<ProducerId>
{
    private Producer(ProducerId id, Name name,  Description description, Cnpj cnpj, Email email, Password password) : base(id)
    {
        Name = name;
        Description = description;
        Cnpj = cnpj;
        Email = email;
        Password = password;
    }
    
    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public Cnpj Cnpj { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }

    public static Result<Producer> Create(Name name, Description description, Cnpj cnpj, Email email, Password password)
    {
        var producer = new Producer(ProducerId.New(), name, description, cnpj, email, password);
        producer.RaiseDomainEvent(new ProducerCreatedDomainEvent(producer.Id));
        return Result.Success(producer);
    }
}