using CleanArquitecture.Domain.Abstrations;

namespace CleanArquitecture.Domain.Entities.Customer.Events;

public record CustomerCreatedDomainEvent(CustomerId CustomerId) : IDomainEvent;
