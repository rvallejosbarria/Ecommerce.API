namespace Ecommerce.Contracts.Requests;

public class UpdateCategoryRequest
{
    public required string Name { get; init; }
    public required string Description { get; init; }
}
