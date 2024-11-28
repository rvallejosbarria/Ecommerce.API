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
}
