namespace Ecommerce.Mapping;

using Contracts.Requests;
using Contracts.Responses;
using Models;

public static class ContractMapping
{
    public static Category MapToCategory(this CreateCategoryRequest request) =>
        new Category { Id = Guid.NewGuid(), Name = request.Name, Description = request.Description };

    public static Category MapToCategory(this UpdateCategoryRequest request, Guid id) =>
        new Category { Id = id, Name = request.Name, Description = request.Description };

    public static CategoryResponse MapToResponse(this Category category) =>
        new CategoryResponse { Id = Guid.NewGuid(), Name = category.Name, Slug = category.Slug, Description = category.Description };

    public static CategoriesResponse MapToResponse(this IEnumerable<Category> categories) =>
        new CategoriesResponse { Categories = categories.Select(MapToResponse) };
}
