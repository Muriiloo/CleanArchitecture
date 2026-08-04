namespace CleanArquitecture.Domain.Abstrations;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; } 
}
