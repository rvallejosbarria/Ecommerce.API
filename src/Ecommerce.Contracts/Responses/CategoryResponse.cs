namespace Ecommerce.Contracts.Responses;

public class CategoryResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
}
