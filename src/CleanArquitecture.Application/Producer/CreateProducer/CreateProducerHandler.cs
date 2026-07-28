using CleanArquitecture.Application.Abstrations.Messaging;
using CleanArquitecture.Domain.Abstrations;
using CleanArquitecture.Domain.Entities.Producer;
using CleanArquitecture.Domain.Entities.Producer.ValueObjects;
using CleanArquitecture.Domain.Entities.Shared.ValueObjects;

namespace CleanArquitecture.Application.Producer.CreateProducer;

public class CreateProducerHandler : ICommandHandler<CreateProducerCommand, Guid>
{
    private readonly IProducerRepository _producerRepo;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProducerHandler(IProducerRepository producerRepo, IUnitOfWork unitOfWork)
    {
        _producerRepo = producerRepo;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid>> Handle(CreateProducerCommand request, CancellationToken cancellationToken)
    {
        var name = Name.Create(request.Name);
        var email = Email.Create(request.Email);
        var password = Password.Create(request.Password);
        var cnpj = Cnpj.Create(request.Cnpj);
        var description = Description.Create(request.Description);

        var producer = Domain.Entities.Producer.Producer.Create(
            name.Value, 
            description.Value, 
            cnpj.Value, 
            email.Value,
            password.Value);
        
        _producerRepo.Add(producer.Value);

        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(producer.Value.Id.Value);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        
    }
}