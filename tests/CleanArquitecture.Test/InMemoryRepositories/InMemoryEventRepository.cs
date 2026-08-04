using CleanArquitecture.Domain.Entities.Event;
using CleanArquitecture.Domain.Shared.Repositories;

namespace CleanArquitecture.Test.InMemoryRepositories;

public sealed class InMemoryEventRepository : IRepository<Event, EventId>, IEventRepository
{
    private readonly List<Event> _events = new();
    public void Add(Event entity)
    {
        _events.Add(entity);
    }

    public Task<Event?> GetByIdAsync(EventId id, CancellationToken cancellationToken)
    {
        var @event = _events.FirstOrDefault(e => e.Id == id);

        return Task.FromResult(@event);
    }
}
