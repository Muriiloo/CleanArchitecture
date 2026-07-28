using CleanArquitecture.Application.Abstrations.Messaging;
using CleanArquitecture.Application.Exceptions;
using CleanArquitecture.Domain.Abstrations;
using CleanArquitecture.Domain.Entities.Producer;
using CleanArquitecture.Domain.Entities.Producer.ValueObjects;
using CleanArquitecture.Domain.Entities.Shared.ValueObjects;
using CleanArquitecture.Domain.Shared.Errors;

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

        var errors = ValidationErrorMapper.GetValidationErrors(name, email, password, cnpj, description);

        if (errors.Any())
            throw new ValidationException(errors);

        List<Error> error = [];
           
        var emailExists = await _producerRepo.GetByEmailAsync(email.Value, cancellationToken);

        if (emailExists is not null)
            error.Add(GlobalErrors.EmailAlreadyExists);


        var cnpjExists = await _producerRepo.GetByCnpjAsync(cnpj.Value, cancellationToken);

        if (cnpjExists is not null)
            error.Add(ProducerErrors.CnpjAlreadyExists);

        if (error.Any())
            return Result.Failures<Guid>(error);

        var producer = Domain.Entities.Producer.Producer.Create(
            name.Value, 
            description.Value, 
            cnpj.Value, 
            email.Value,
            password.Value);

        if (producer.IsFailure)
            return Result.Failure<Guid>(producer.Error);
        
        _producerRepo.Add(producer.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(producer.Value.Id.Value);
    }
}