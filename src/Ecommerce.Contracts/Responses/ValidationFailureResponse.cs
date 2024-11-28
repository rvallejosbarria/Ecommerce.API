namespace Ecommerce.Contracts.Responses;

public class ValidationFailureResponse
{
    public IEnumerable<ValidationResponse> Errors { get; init; }
}

public class ValidationResponse
{
    public required string PropertyName { get; init; }
    public required string Message { get; init; }
}
