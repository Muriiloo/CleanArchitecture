using CleanArquitecture.Domain.Abstrations;

namespace CleanArquitecture.Domain.Shared.Repositories;

public interface IRepository<TEntity, TEntityId> where TEntity : Entity<TEntityId> where TEntityId : class
{
    void Add(TEntity entity);

    Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken cancellationToken);
}
