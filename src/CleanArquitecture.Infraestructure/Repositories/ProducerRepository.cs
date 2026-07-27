using CleanArquitecture.Domain.Entities.Producer;
using CleanArquitecture.Infraestructure.Context;

namespace CleanArquitecture.Infraestructure.Repositories;

internal sealed class ProducerRepository : Repository<Producer, ProducerId> , IProducerRepository
{
    private readonly AppDbContext _dbContext;

    public ProducerRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}