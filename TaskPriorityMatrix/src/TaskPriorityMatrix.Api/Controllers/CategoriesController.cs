// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Mvc;
using TaskPriorityMatrix.Api.Features.Categories;

namespace TaskPriorityMatrix.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<CategoryDto>> GetCategories()
    {
        // Placeholder: Return empty list for now
        return Ok(new List<CategoryDto>());
    }

    [HttpGet("{id}")]
    public ActionResult<CategoryDto> GetCategory(Guid id)
    {
        // Placeholder: Return null for now
        return NotFound();
    }

    [HttpPost]
    public ActionResult<CategoryDto> CreateCategory([FromBody] CreateCategoryCommand command)
    {
        // Placeholder: Return created category
        var category = new CategoryDto
        {
            CategoryId = Guid.NewGuid(),
            Name = command.Name,
            Color = command.Color,
            CreatedAt = DateTime.UtcNow
        };
        return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, category);
    }

    [HttpPut("{id}")]
    public ActionResult<CategoryDto> UpdateCategory(Guid id, [FromBody] UpdateCategoryCommand command)
    {
        // Placeholder: Return updated category
        var category = new CategoryDto
        {
            CategoryId = command.CategoryId,
            Name = command.Name,
            Color = command.Color,
            CreatedAt = DateTime.UtcNow
        };
        return Ok(category);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCategory(Guid id)
    {
        // Placeholder: Return success
        return NoContent();
    }
}
