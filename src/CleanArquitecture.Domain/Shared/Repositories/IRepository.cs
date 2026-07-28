namespace CleanArquitecture.Domain.Shared.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    void Add(TEntity entity);
}
