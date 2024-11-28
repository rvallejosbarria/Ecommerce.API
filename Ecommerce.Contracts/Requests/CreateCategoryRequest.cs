namespace Ecommerce.Contracts.Requests;

public class CreateCategoryRequest
{
    public required string Name { get; init; }
    public required string Description { get; init; }
}
