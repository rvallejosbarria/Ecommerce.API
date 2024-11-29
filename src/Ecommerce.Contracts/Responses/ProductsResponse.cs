namespace Ecommerce.Contracts.Responses;

public class ProductsResponse
{
    public required IEnumerable<ProductResponse> Products { get; init; } = Enumerable.Empty<ProductResponse>();
}
