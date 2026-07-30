using CleanArquitecture.Domain.Abstrations;

namespace CleanArquitecture.Infraestructure.Repositories.InMemoryRepositories;

public sealed class InMemoryUnitOfWorkRepository : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return Task.FromResult(1);
    }
}
