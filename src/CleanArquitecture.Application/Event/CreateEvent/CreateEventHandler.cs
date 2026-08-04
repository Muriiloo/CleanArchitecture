using CleanArquitecture.Application.Abstrations.Messaging;
using CleanArquitecture.Application.Exceptions;
using CleanArquitecture.Domain.Abstrations;
using CleanArquitecture.Domain.Entities.Event;
using CleanArquitecture.Domain.Entities.Event.ValueObjects;

namespace CleanArquitecture.Application.Event.CreateEvent;

public class CreateEventHandler : ICommandHandler<CreateEventCommand, Guid>
{
    private readonly IEventRepository _eventRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _time;

    public CreateEventHandler(IEventRepository eventRepo, IUnitOfWork unitOfWork, IDateTimeProvider time)
    {
        _eventRepo = eventRepo;
        _unitOfWork = unitOfWork;
        _time = time;
    }

    public async Task<Result<Guid>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var nameEvent = NameEvent.Create(request.NameEvent);
        var descriptionEvent = DescriptionEvent.Create(request.DescriptionEvent);
        var address = Address.Create(request.Location, request.City, request.State);
        var periodEvent = PeriodEvent.Create(request.StartDate, request.EndDate, _time);
        var limitAge = LimitAge.Create(request.LimitAge);
        var aboutEvent = AboutEvent.Create(request.AboutEvent);

        var errors = ValidationErrorMapper.GetValidationErrors(nameEvent, descriptionEvent, address, periodEvent, limitAge, aboutEvent);

        if (errors.Any())
            throw new ValidationException(errors);

        var @event = Domain.Entities.Event.Event.Create(
            nameEvent.Value,
            descriptionEvent.Value,
            address.Value,
            periodEvent.Value,
            limitAge.Value,
            aboutEvent.Value);

        if (@event.IsFailure)
            return Result.Failure<Guid>(@event.Error);

        _eventRepo.Add(@event.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(@event.Value.Id.Value);
    }
}
