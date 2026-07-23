namespace CleanArquitecture.Domain.Abstrations;

public interface IEntity
{
    void ClearDomainEvents();
    IReadOnlyList<IDomainEvent> GetDomainEvents();
}
