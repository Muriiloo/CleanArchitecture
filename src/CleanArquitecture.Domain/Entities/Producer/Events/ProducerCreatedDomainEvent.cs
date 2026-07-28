using CleanArquitecture.Domain.Abstrations;

namespace CleanArquitecture.Domain.Entities.Producer.Events;

public record ProducerCreatedDomainEvent(ProducerId ProducerId) : IDomainEvent;