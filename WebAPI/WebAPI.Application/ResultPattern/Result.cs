namespace WebAPI.Application.ResultPattern;

public class Result<T>
{
    public T Value { get; }

    public bool IsFailure { get; }

    public IEnumerable<Error> Errors { get; }

    public Result( T value )
    {
        Value = value;
        IsFailure = false;
    }

    public Result( IEnumerable<Error> errors )
    {
        Errors = errors;
        IsFailure = true;
    }

    public static Result<T> Success( T value ) => new( value );

    public static Result<T> Failure( Error error ) => new( [ error ] );

    public static Result<T> Failure( IEnumerable<Error> errors ) => new( errors );
}

public class Result
{
    public bool IsFailure { get; }
    public IEnumerable<Error> Errors { get; }

    protected Result( bool isSuccess, IEnumerable<Error> errors )
    {
        IsFailure = isSuccess;
        Errors = errors;
    }

    public static Result Success() => new( false, Array.Empty<Error>() );
    public static Result Failure( Error error ) => new( true, [ error ] );
    public static Result Failure( IEnumerable<Error> errors ) => new( true, errors );
}