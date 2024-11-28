namespace Ecommerce.Controllers;

using Application.Repositories;
using Contracts.Requests;
using Mapping;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ILogger<CategoriesController> _logger;
    private readonly ICategoryRepository _categoryRepository;

    public CategoriesController(ILogger<CategoriesController> logger, ICategoryRepository categoryRepository)
    {
        _logger = logger;
        _categoryRepository = categoryRepository;
    }

    [HttpPost(ApiEndpoints.Categories.Create)]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
    {
        var category = request.MapToCategory();

        await _categoryRepository.CreateAsync(category);

        return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
    }

    [HttpGet(ApiEndpoints.Categories.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null) { return NotFound(); }
        return Ok(category.MapToResponse());
    }

    [HttpGet(ApiEndpoints.Categories.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return Ok(categories);
    }

    [HttpPut(ApiEndpoints.Categories.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategoryRequest request)
    {
        var category = request.MapToCategory(id);
        var updated = await _categoryRepository.UpdateAsync(category);
        if (!updated) { return NotFound(); }
        return Ok(category.MapToResponse());
    }

    [HttpDelete(ApiEndpoints.Categories.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var removed = await _categoryRepository.DeleteAsync(id);
        if (!removed) { return NotFound(); }
        return Ok();
    }
}
