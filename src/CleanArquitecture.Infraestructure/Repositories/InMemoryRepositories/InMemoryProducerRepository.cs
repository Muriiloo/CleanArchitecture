using CleanArquitecture.Domain.Entities.Producer;
using CleanArquitecture.Domain.Entities.Producer.ValueObjects;
using CleanArquitecture.Domain.Entities.Shared.ValueObjects;

namespace CleanArquitecture.Infraestructure.Repositories.InMemoryRepositories;

public sealed class InMemoryProducerRepository : Repository<Producer>, IProducerRepository
{
    private readonly List<Producer> _producers = [];
    public void Add(Producer entity)
    {
        _producers.Add(entity);
    }

    public Task<Producer?> GetByCnpjAsync(Cnpj cnpj, CancellationToken cancellationToken)
    {
        var producer =  _producers.FirstOrDefault(p => p.Cnpj == cnpj);

        return Task.FromResult(producer);
    }

    public Task<Producer?> GetByEmailAsync(Email email, CancellationToken cancellationToken)
    {
        var producer = _producers.FirstOrDefault(p => p.Email == email);

        return Task.FromResult(producer);
    }
}
