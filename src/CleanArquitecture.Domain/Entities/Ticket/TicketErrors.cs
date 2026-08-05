using CleanArquitecture.Domain.Abstrations;

namespace CleanArquitecture.Domain.Entities.Ticket;

public class TicketErrors
{
    public static Error InvalidPrice => new("400", "Invalid price.");
}
