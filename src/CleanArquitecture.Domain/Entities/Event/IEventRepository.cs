using CleanArquitecture.Domain.Shared.Repositories;

namespace CleanArquitecture.Domain.Entities.Event;

public interface IEventRepository : IRepository<Event, EventId>
{
}
