namespace CleanArquitecture.Domain.Abstrations;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
