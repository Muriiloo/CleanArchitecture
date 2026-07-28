using CleanArquitecture.Domain.Abstrations;
using CleanArquitecture.Domain.Entities.Shared.ValueObjects;
using CleanArquitecture.Domain.Shared.Repositories;
using CleanArquitecture.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArquitecture.Infraestructure.Repositories;

internal abstract class Repository<TEntity, TEntityId> : IRepository<TEntity> where TEntity : Entity<TEntityId> where TEntityId : class
{
    protected readonly AppDbContext _db;

    public Repository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken cancellationToken = default)
    {
        return await _db.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public void Add(TEntity entity)
    {
        _db.Add(entity);
    }

}
