namespace Ecommerce.Controllers;

using Application.Services;
using Contracts.Requests;
using Mapping;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ILogger<CategoriesController> _logger;
    private readonly ICategoryService _categoryService;

    public CategoriesController(ILogger<CategoriesController> logger, ICategoryService categoryRepository)
    {
        _logger = logger;
        _categoryService = categoryRepository;
    }

    [HttpPost(ApiEndpoints.Categories.Create)]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
    {
        var category = request.MapToCategory();

        await _categoryService.CreateAsync(category);

        return CreatedAtAction(nameof(Get), new { idOrSlug = category.Id }, category);
    }

    [HttpGet(ApiEndpoints.Categories.Get)]
    public async Task<IActionResult> Get([FromRoute] string idOrSlug)
    {
        var category = Guid.TryParse(idOrSlug, out var id)
            ? await _categoryService.GetByIdAsync(id)
            : await _categoryService.GetBySlugAsync(idOrSlug);
        if (category is null) { return NotFound(); }
        return Ok(category.MapToResponse());
    }

    [HttpGet(ApiEndpoints.Categories.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();
        return Ok(categories);
    }

    [HttpPut(ApiEndpoints.Categories.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategoryRequest request)
    {
        var category = request.MapToCategory(id);
        var updated = await _categoryService.UpdateAsync(category);
        if (updated is null) { return NotFound(); }
        return Ok(category.MapToResponse());
    }

    [HttpDelete(ApiEndpoints.Categories.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var removed = await _categoryService.DeleteAsync(id);
        if (!removed) { return NotFound(); }
        return Ok();
    }
}
