// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using PasswordAccountAuditor.Api.Features.Account;

namespace PasswordAccountAuditor.Api.Controllers;

/// <summary>
/// Controller for managing accounts.
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
    /// <returns>List of accounts.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<AccountDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<AccountDto>>> GetAccounts()
    {
        _logger.LogInformation("Getting all accounts");
        var accounts = await _mediator.Send(new GetAccountsQuery());
        return Ok(accounts);
    }

    /// <summary>
    /// Gets an account by ID.
    /// </summary>
    /// <param name="id">The account ID.</param>
    /// <returns>The account.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AccountDto>> GetAccountById(Guid id)
    {
        _logger.LogInformation("Getting account with ID: {AccountId}", id);
        var account = await _mediator.Send(new GetAccountByIdQuery(id));

        if (account == null)
        {
            _logger.LogWarning("Account with ID {AccountId} not found", id);
            return NotFound();
        }

        return Ok(account);
    }

    /// <summary>
    /// Creates a new account.
    /// </summary>
    /// <param name="command">The create account command.</param>
    /// <returns>The created account.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AccountDto>> CreateAccount([FromBody] CreateAccountCommand command)
    {
        _logger.LogInformation("Creating new account: {AccountName}", command.AccountName);
        var account = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAccountById), new { id = account.AccountId }, account);
    }

    /// <summary>
    /// Updates an existing account.
    /// </summary>
    /// <param name="id">The account ID.</param>
    /// <param name="command">The update account command.</param>
    /// <returns>The updated account.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AccountDto>> UpdateAccount(Guid id, [FromBody] UpdateAccountCommand command)
    {
        if (id != command.AccountId)
        {
            _logger.LogWarning("Account ID mismatch: {UrlId} vs {CommandId}", id, command.AccountId);
            return BadRequest("Account ID mismatch");
        }

        _logger.LogInformation("Updating account with ID: {AccountId}", id);

        try
        {
            var account = await _mediator.Send(command);
            return Ok(account);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Failed to update account with ID: {AccountId}", id);
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Deletes an account.
    /// </summary>
    /// <param name="id">The account ID.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAccount(Guid id)
    {
        _logger.LogInformation("Deleting account with ID: {AccountId}", id);

        try
        {
            await _mediator.Send(new DeleteAccountCommand(id));
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Failed to delete account with ID: {AccountId}", id);
            return NotFound(ex.Message);
        }
    }
}
