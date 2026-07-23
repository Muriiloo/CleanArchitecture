namespace CleanArquitecture.Domain.Entities.Customer;

public record CustomerId(Guid Value)
{
    public static CustomerId FromValue(Guid Value) => new CustomerId(Value);
    public static CustomerId New() => FromValue(Guid.NewGuid());
}
