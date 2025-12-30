// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Api.Features.Account;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPortfolioTracker.Api.Controllers;

/// <summary>
/// Controller for managing investment accounts.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AccountController> _logger;

    public AccountController(IMediator mediator, ILogger<AccountController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all accounts.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<AccountDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<AccountDto>>> GetAccounts(CancellationToken cancellationToken)
    {
        var accounts = await _mediator.Send(new GetAccountsQuery(), cancellationToken);
        return Ok(accounts);
    }

    /// <summary>
    /// Gets an account by ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AccountDto>> GetAccountById(Guid id, CancellationToken cancellationToken)
    {
        var account = await _mediator.Send(new GetAccountByIdQuery(id), cancellationToken);

        if (account == null)
        {
            return NotFound();
        }

        return Ok(account);
    }

    /// <summary>
    /// Creates a new account.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AccountDto>> CreateAccount(
        [FromBody] CreateAccountCommand command,
        CancellationToken cancellationToken)
    {
        var account = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetAccountById), new { id = account.AccountId }, account);
    }

    /// <summary>
    /// Updates an existing account.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AccountDto>> UpdateAccount(
        Guid id,
        [FromBody] UpdateAccountCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.AccountId)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var account = await _mediator.Send(command, cancellationToken);
            return Ok(account);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Deletes an account.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAccount(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteAccountCommand(id), cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
