namespace Ecommerce.Contracts.Responses;

using System.Collections;

public class CategoriesResponse
{
    public required IEnumerable<CategoryResponse> Categories { get; init; } = Enumerable.Empty<CategoryResponse>();
}
