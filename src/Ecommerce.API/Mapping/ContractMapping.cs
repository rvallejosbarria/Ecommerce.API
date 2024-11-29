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
        new CategoryResponse { Id = category.Id, Name = category.Name, Slug = category.Slug, Description = category.Description };

    public static CategoriesResponse MapToResponse(this IEnumerable<Category> categories) =>
        new CategoriesResponse { Categories = categories.Select(MapToResponse) };

    public static Product MapToProduct(this CreateProductRequest request) =>
        new Product { Id = Guid.NewGuid(), Name = request.Name, Description = request.Description, Price = request.Price, Stock = request.Stock, Image = request.Image };

    public static Product MapToProduct(this UpdateProductRequest request, Guid id) =>
        new Product { Id = id, Name = request.Name, Description = request.Description, Price = request.Price, Stock = request.Stock, Image = request.Image };

    public static ProductResponse MapToResponse(this Product product) =>
        new ProductResponse { Id = product.Id, Name = product.Name, Slug = product.Slug, Description = product.Description, Price = product.Price, Stock = product.Stock, Image = product.Image };

    public static ProductsResponse MapToResponse(this IEnumerable<Product> products) =>
        new ProductsResponse { Products = products.Select(MapToResponse) };
}
