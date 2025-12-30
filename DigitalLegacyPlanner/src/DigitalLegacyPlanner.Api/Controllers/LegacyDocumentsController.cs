// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Api.Features.LegacyDocuments;
using DigitalLegacyPlanner.Api.Features.LegacyDocuments.Commands;
using DigitalLegacyPlanner.Api.Features.LegacyDocuments.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLegacyPlanner.Api.Controllers;

/// <summary>
/// Controller for managing legacy documents.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LegacyDocumentsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<LegacyDocumentsController> _logger;

    public LegacyDocumentsController(IMediator mediator, ILogger<LegacyDocumentsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all legacy documents.
    /// </summary>
    /// <param name="userId">Optional user ID filter.</param>
    /// <param name="documentType">Optional document type filter.</param>
    /// <param name="needsReview">Optional needs review filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of legacy documents.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<LegacyDocumentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<LegacyDocumentDto>>> GetLegacyDocuments(
        [FromQuery] Guid? userId,
        [FromQuery] string? documentType,
        [FromQuery] bool? needsReview,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting legacy documents for user {UserId}", userId);
        var query = new GetLegacyDocumentsQuery
        {
            UserId = userId,
            DocumentType = documentType,
            NeedsReview = needsReview
        };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets a legacy document by ID.
    /// </summary>
    /// <param name="id">Legacy document ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The legacy document.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LegacyDocumentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LegacyDocumentDto>> GetLegacyDocumentById(
        Guid id,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting legacy document {LegacyDocumentId}", id);
        var query = new GetLegacyDocumentByIdQuery { LegacyDocumentId = id };
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new legacy document.
    /// </summary>
    /// <param name="command">Create command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created legacy document.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(LegacyDocumentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LegacyDocumentDto>> CreateLegacyDocument(
        [FromBody] CreateLegacyDocumentCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating legacy document for user {UserId}", command.UserId);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetLegacyDocumentById), new { id = result.LegacyDocumentId }, result);
    }

    /// <summary>
    /// Updates an existing legacy document.
    /// </summary>
    /// <param name="id">Legacy document ID.</param>
    /// <param name="command">Update command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated legacy document.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(LegacyDocumentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LegacyDocumentDto>> UpdateLegacyDocument(
        Guid id,
        [FromBody] UpdateLegacyDocumentCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.LegacyDocumentId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating legacy document {LegacyDocumentId}", id);
        var result = await _mediator.Send(command, cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a legacy document.
    /// </summary>
    /// <param name="id">Legacy document ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteLegacyDocument(
        Guid id,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting legacy document {LegacyDocumentId}", id);
        var command = new DeleteLegacyDocumentCommand { LegacyDocumentId = id };
        var result = await _mediator.Send(command, cancellationToken);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
