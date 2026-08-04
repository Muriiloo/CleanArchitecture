using CleanArquitecture.Domain.Abstrations;

namespace CleanArquitecture.Domain.Entities.Event.Events;

public record EventCreatedDomainEvent(EventId EventId) : IDomainEvent;
