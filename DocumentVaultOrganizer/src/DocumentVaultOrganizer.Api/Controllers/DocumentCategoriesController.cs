// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DocumentVaultOrganizer.Api.Features.DocumentCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocumentVaultOrganizer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentCategoriesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DocumentCategoriesController> _logger;

    public DocumentCategoriesController(IMediator mediator, ILogger<DocumentCategoriesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<DocumentCategoryDto>>> GetDocumentCategories()
    {
        try
        {
            var query = new GetDocumentCategories.Query();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving document categories");
            return StatusCode(500, "An error occurred while retrieving document categories");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DocumentCategoryDto>> GetDocumentCategoryById(Guid id)
    {
        try
        {
            var query = new GetDocumentCategoryById.Query { DocumentCategoryId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving document category {CategoryId}", id);
            return StatusCode(500, "An error occurred while retrieving the document category");
        }
    }

    [HttpPost]
    public async Task<ActionResult<DocumentCategoryDto>> CreateDocumentCategory([FromBody] CreateDocumentCategory.Command command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetDocumentCategoryById), new { id = result.DocumentCategoryId }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating document category");
            return StatusCode(500, "An error occurred while creating the document category");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DocumentCategoryDto>> UpdateDocumentCategory(Guid id, [FromBody] UpdateDocumentCategory.Command command)
    {
        try
        {
            if (id != command.DocumentCategoryId)
            {
                return BadRequest("Category ID mismatch");
            }

            var result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating document category {CategoryId}", id);
            return StatusCode(500, "An error occurred while updating the document category");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDocumentCategory(Guid id)
    {
        try
        {
            var command = new DeleteDocumentCategory.Command { DocumentCategoryId = id };
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting document category {CategoryId}", id);
            return StatusCode(500, "An error occurred while deleting the document category");
        }
    }
}
