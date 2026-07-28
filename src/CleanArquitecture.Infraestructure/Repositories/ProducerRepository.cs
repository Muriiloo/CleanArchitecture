using CleanArquitecture.Domain.Entities.Producer;
using CleanArquitecture.Domain.Entities.Producer.ValueObjects;
using CleanArquitecture.Domain.Entities.Shared.ValueObjects;
using CleanArquitecture.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArquitecture.Infraestructure.Repositories;

internal sealed class ProducerRepository : Repository<Producer, ProducerId>, IProducerRepository
{
    private readonly AppDbContext _dbContext;

    public ProducerRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Producer?> GetByCnpjAsync(Cnpj cnpj, CancellationToken cancellationToken)
    {
        return await _db.Set<Producer>().FirstOrDefaultAsync(p => p.Cnpj == cnpj, cancellationToken);
    }

    public async Task<Producer?> GetByEmailAsync(Email email, CancellationToken cancellationToken)
    {
        return await _db.Set<Producer>().FirstOrDefaultAsync(p => p.Email == email, cancellationToken);
    }
}