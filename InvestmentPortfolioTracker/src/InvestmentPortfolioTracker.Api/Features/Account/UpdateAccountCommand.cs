// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioTracker.Api.Features.Account;

/// <summary>
/// Command to update an existing account.
/// </summary>
public record UpdateAccountCommand : IRequest<AccountDto>
{
    public Guid AccountId { get; set; }
    public string Name { get; set; } = string.Empty;
    public AccountType AccountType { get; set; }
    public string Institution { get; set; } = string.Empty;
    public string? AccountNumber { get; set; }
    public decimal CurrentBalance { get; set; }
    public bool IsActive { get; set; }
    public DateTime OpenedDate { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Handler for UpdateAccountCommand.
/// </summary>
public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, AccountDto>
{
    private readonly IInvestmentPortfolioTrackerContext _context;
    private readonly ILogger<UpdateAccountCommandHandler> _logger;

    public UpdateAccountCommandHandler(
        IInvestmentPortfolioTrackerContext context,
        ILogger<UpdateAccountCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<AccountDto> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating account: {AccountId}", request.AccountId);

        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.AccountId == request.AccountId, cancellationToken);

        if (account == null)
        {
            _logger.LogWarning("Account not found: {AccountId}", request.AccountId);
            throw new KeyNotFoundException($"Account with ID {request.AccountId} not found.");
        }

        account.Name = request.Name;
        account.AccountType = request.AccountType;
        account.Institution = request.Institution;
        account.AccountNumber = request.AccountNumber;
        account.CurrentBalance = request.CurrentBalance;
        account.IsActive = request.IsActive;
        account.OpenedDate = request.OpenedDate;
        account.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Account updated: {AccountId}", account.AccountId);

        return account.ToDto();
    }
}
