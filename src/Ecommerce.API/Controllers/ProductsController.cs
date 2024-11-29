namespace Ecommerce.Controllers;

using Application.Services;
using Contracts.Requests;
using Mapping;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IProductService _productService;

    public ProductsController(ILogger<ProductsController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    [HttpPost(ApiEndpoints.Products.Create)]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var product = request.MapToProduct();

        await _productService.CreateAsync(product, cancellationToken);

        return CreatedAtAction(nameof(Get), new { idOrSlug = product.Id }, product);
    }

    [HttpGet(ApiEndpoints.Products.Get)]
    public async Task<IActionResult> Get([FromRoute] string idOrSlug, CancellationToken cancellationToken)
    {
        var product = Guid.TryParse(idOrSlug, out var id)
            ? await _productService.GetByIdAsync(id, cancellationToken)
            : await _productService.GetBySlugAsync(idOrSlug, cancellationToken);
        if (product is null) { return NotFound(); }

        return Ok(product.MapToResponse());
    }

    [HttpGet(ApiEndpoints.Products.GetAll)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var products = await _productService.GetAllAsync(cancellationToken);
        return Ok(products);
    }
}
