// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Api.Features.Transaction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPortfolioTracker.Api.Controllers;

/// <summary>
/// Controller for managing investment transactions.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TransactionController> _logger;

    public TransactionController(IMediator mediator, ILogger<TransactionController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all transactions.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<TransactionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TransactionDto>>> GetTransactions(CancellationToken cancellationToken)
    {
        var transactions = await _mediator.Send(new GetTransactionsQuery(), cancellationToken);
        return Ok(transactions);
    }

    /// <summary>
    /// Gets a transaction by ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TransactionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TransactionDto>> GetTransactionById(Guid id, CancellationToken cancellationToken)
    {
        var transaction = await _mediator.Send(new GetTransactionByIdQuery(id), cancellationToken);

        if (transaction == null)
        {
            return NotFound();
        }

        return Ok(transaction);
    }

    /// <summary>
    /// Creates a new transaction.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(TransactionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TransactionDto>> CreateTransaction(
        [FromBody] CreateTransactionCommand command,
        CancellationToken cancellationToken)
    {
        var transaction = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.TransactionId }, transaction);
    }

    /// <summary>
    /// Updates an existing transaction.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(TransactionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TransactionDto>> UpdateTransaction(
        Guid id,
        [FromBody] UpdateTransactionCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.TransactionId)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var transaction = await _mediator.Send(command, cancellationToken);
            return Ok(transaction);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Deletes a transaction.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTransaction(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteTransactionCommand(id), cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
