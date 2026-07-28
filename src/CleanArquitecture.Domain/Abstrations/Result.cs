using System.Diagnostics.CodeAnalysis;

namespace CleanArquitecture.Domain.Abstrations;

public class Result
{
    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
            throw new InvalidCastException();

        if (!isSuccess && error == Error.None)
            throw new InvalidCastException();

        IsSuccess = isSuccess;
        Error = error;
        Errors = isSuccess ? [] : [error];
    }
    protected internal Result(bool isSuccess, List<Error> errors)
    {
        if (isSuccess && errors.Count > 0)
            throw new InvalidOperationException();

        if (!isSuccess && errors.Count == 0)
            throw new InvalidOperationException();

        IsSuccess = isSuccess;
        Errors = errors;
        Error = isSuccess ? Error.None : errors[0];
    }


    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }
    public IReadOnlyList<Error> Errors { get; }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    public static Result Failures(List<Error> errors) => new(false, errors);
    public static Result<T> Failures<T>(List<Error> errors) => new(default!, false, errors);
    public static Result<T> Success<T>(T value) => new(value, true, Error.None);
    public static Result<T> Failure<T>(Error error) => new(default!, false, error);
}

public class Result<T> : Result
{
    private readonly T? _value;

    protected internal Result(T value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        _value = value;
    }

    protected internal Result(T value, bool isSuccess, List<Error> errors) : base(isSuccess, errors)
    {
        _value = value;
    }

    [NotNull]
    public T Value => IsSuccess ? _value! : throw new InvalidOperationException();
}
