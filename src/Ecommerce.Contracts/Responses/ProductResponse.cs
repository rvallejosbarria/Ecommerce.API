namespace Ecommerce.Contracts.Responses;

public class ProductResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Slug { get; init; }
    public required string Description { get; init; }
    public required decimal Price { get; init; }
    public required int Stock { get; init; }
    public required string Image { get; init; }
    // public Guid CategoryId { get; set; }
    // public Category Category { get; set; } = null!;
}
