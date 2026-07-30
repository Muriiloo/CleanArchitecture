using CleanArquitecture.Domain.Entities.Producer;
using CleanArquitecture.Domain.Entities.Producer.ValueObjects;
using CleanArquitecture.Domain.Entities.Shared.ValueObjects;
using CleanArquitecture.Domain.Shared.Repositories;

namespace CleanArquitecture.Infraestructure.Repositories.InMemoryRepositories;

public sealed class InMemoryProducerRepository : IRepository<Producer, ProducerId>, IProducerRepository
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

    public Task<Producer?> GetByIdAsync(ProducerId id, CancellationToken cancellationToken)
    {
        var producer = _producers.FirstOrDefault(p => p.Id == id);

        return Task.FromResult(producer);
    }
}
