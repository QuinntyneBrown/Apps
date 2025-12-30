// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Api.Features.Invoices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceProjectManager.Api.Controllers;

/// <summary>
/// Controller for managing invoices.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="InvoicesController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    public InvoicesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets all invoices for the current user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of invoices.</returns>
    [HttpGet]
    public async Task<ActionResult<List<InvoiceDto>>> GetInvoices([FromQuery] Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetInvoicesQuery { UserId = userId };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets an invoice by ID.
    /// </summary>
    /// <param name="id">The invoice ID.</param>
    /// <param name="userId">The user ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The invoice.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<InvoiceDto>> GetInvoice(Guid id, [FromQuery] Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetInvoiceByIdQuery { InvoiceId = id, UserId = userId };
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new invoice.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created invoice.</returns>
    [HttpPost]
    public async Task<ActionResult<InvoiceDto>> CreateInvoice([FromBody] CreateInvoiceCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetInvoice), new { id = result.InvoiceId, userId = result.UserId }, result);
    }

    /// <summary>
    /// Updates an invoice.
    /// </summary>
    /// <param name="id">The invoice ID.</param>
    /// <param name="command">The update command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated invoice.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<InvoiceDto>> UpdateInvoice(Guid id, [FromBody] UpdateInvoiceCommand command, CancellationToken cancellationToken)
    {
        if (id != command.InvoiceId)
        {
            return BadRequest("ID mismatch");
        }

        var result = await _mediator.Send(command, cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes an invoice.
    /// </summary>
    /// <param name="id">The invoice ID.</param>
    /// <param name="userId">The user ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteInvoice(Guid id, [FromQuery] Guid userId, CancellationToken cancellationToken)
    {
        var command = new DeleteInvoiceCommand { InvoiceId = id, UserId = userId };
        var result = await _mediator.Send(command, cancellationToken);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
