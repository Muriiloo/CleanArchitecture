namespace CleanArquitecture.Domain.Entities.Producer;

public record ProducerId(Guid Value)
{
    public static ProducerId FromValue(Guid value) => new ProducerId(value);
    public static ProducerId New() => FromValue(Guid.NewGuid());
}