namespace Ecommerce.Application.Services;

using FluentValidation;
using Models;
using Repositories;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IValidator<Product> _productValidator;

    public ProductService(IProductRepository productRepository, IValidator<Product> productValidator)
    {
        _productRepository = productRepository;
        _productValidator = productValidator;
    }

    public async Task<bool> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _productValidator.ValidateAndThrowAsync(product, cancellationToken);
        return await _productRepository.CreateAsync(product, cancellationToken);
    }

    public Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _productRepository.GetByIdAsync(id, cancellationToken);

    public Task<Product?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default) =>
        _productRepository.GetBySlugAsync(slug, cancellationToken);

    public Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _productRepository.GetAllAsync(cancellationToken);

    public async Task<Product?> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _productValidator.ValidateAndThrowAsync(product, cancellationToken);
        var productExists = await _productRepository.ExistsByIdAsync(product.Id, cancellationToken);
        if (!productExists)
        {
            return null;
        }

        await _productRepository.UpdateAsync(product, cancellationToken);
        return product;
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default) =>
        _productRepository.DeleteAsync(id, cancellationToken);
}
