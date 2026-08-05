using CleanArquitecture.Domain.Abstrations;
using CleanArquitecture.Domain.Entities.Customer;
using CleanArquitecture.Domain.Entities.Event;

namespace CleanArquitecture.Domain.Entities.Ticket;

public sealed class Ticket : Entity<TicketId>
{
    private Ticket(TicketId id) : base(id)
    {

    }

    public EventId EventId { get; private set; }
    public CustomerId CustomerId { get; private set; }
    public decimal Price { get; private set; }
}
