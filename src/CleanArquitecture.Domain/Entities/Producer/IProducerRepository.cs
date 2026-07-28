using CleanArquitecture.Domain.Entities.Producer.ValueObjects;
using CleanArquitecture.Domain.Entities.Shared.ValueObjects;
using CleanArquitecture.Domain.Shared.Repositories;

namespace CleanArquitecture.Domain.Entities.Producer;

public interface IProducerRepository : IRepository<Producer>
{
    Task<Producer?> GetByEmailAsync(Email email, CancellationToken cancellationToken);
    Task<Producer?> GetByCnpjAsync(Cnpj cnpj, CancellationToken cancellationToken);
}