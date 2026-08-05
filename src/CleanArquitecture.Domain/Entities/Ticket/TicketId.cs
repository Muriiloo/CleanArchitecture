namespace CleanArquitecture.Domain.Entities.Ticket;

public record TicketId(Guid Value)
{
    public static TicketId FromValue(Guid value) => new TicketId(value);
    public static TicketId New() => FromValue(Guid.NewGuid());
}
