namespace Ecommerce.Application.Validators;

using FluentValidation;
using Models;
using Repositories;

public class ProductValidator : AbstractValidator<Product>
{
    private readonly IProductRepository _productRepository;

    public ProductValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Price).GreaterThanOrEqualTo(10_000);
        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Slug).MustAsync(ValidateSlug).WithMessage("This category already exists");
    }

    private async Task<bool> ValidateSlug(Product product, string slug, CancellationToken cancellationToken)
    {
        var existingProduct = await _productRepository.GetBySlugAsync(slug);

        if (existingProduct is not null)
        {
            return existingProduct.Id == product.Id;
        }
        return existingProduct is null;
    }
}
