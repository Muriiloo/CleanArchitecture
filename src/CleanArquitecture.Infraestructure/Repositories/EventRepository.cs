using CleanArquitecture.Domain.Entities.Event;
using CleanArquitecture.Infraestructure.Context;

namespace CleanArquitecture.Infraestructure.Repositories;

internal sealed class EventRepository : Repository<Event, EventId> , IEventRepository
{
    private readonly AppDbContext _dbContext;

    public EventRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
