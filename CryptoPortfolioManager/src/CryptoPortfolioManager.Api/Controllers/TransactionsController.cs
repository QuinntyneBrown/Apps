// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Api.Features.Transactions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPortfolioManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransactionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<TransactionDto>>> GetTransactions([FromQuery] Guid? walletId = null)
    {
        var transactions = await _mediator.Send(new GetTransactionsQuery { WalletId = walletId });
        return Ok(transactions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TransactionDto>> GetTransaction(Guid id)
    {
        var transaction = await _mediator.Send(new GetTransactionByIdQuery { TransactionId = id });

        if (transaction == null)
            return NotFound();

        return Ok(transaction);
    }

    [HttpPost]
    public async Task<ActionResult<TransactionDto>> CreateTransaction([FromBody] CreateTransactionCommand command)
    {
        var transaction = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTransaction), new { id = transaction.TransactionId }, transaction);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TransactionDto>> UpdateTransaction(Guid id, [FromBody] UpdateTransactionCommand command)
    {
        command.TransactionId = id;
        var transaction = await _mediator.Send(command);

        if (transaction == null)
            return NotFound();

        return Ok(transaction);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTransaction(Guid id)
    {
        var result = await _mediator.Send(new DeleteTransactionCommand { TransactionId = id });

        if (!result)
            return NotFound();

        return NoContent();
    }
}
