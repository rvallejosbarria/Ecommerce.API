namespace Ecommerce.Contracts.Requests;

public class UpdateProductRequest
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required decimal Price { get; init; }
    public required int Stock { get; init; }
    public string? Image { get; init; }
    // public Guid CategoryId { get; set; }
    // public Category Category { get; set; } = null!;
}
