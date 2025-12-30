// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioTracker.Api.Features.Account;

/// <summary>
/// Command to delete an account.
/// </summary>
public record DeleteAccountCommand(Guid AccountId) : IRequest<Unit>;

/// <summary>
/// Handler for DeleteAccountCommand.
/// </summary>
public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Unit>
{
    private readonly IInvestmentPortfolioTrackerContext _context;
    private readonly ILogger<DeleteAccountCommandHandler> _logger;

    public DeleteAccountCommandHandler(
        IInvestmentPortfolioTrackerContext context,
        ILogger<DeleteAccountCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting account: {AccountId}", request.AccountId);

        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.AccountId == request.AccountId, cancellationToken);

        if (account == null)
        {
            _logger.LogWarning("Account not found: {AccountId}", request.AccountId);
            throw new KeyNotFoundException($"Account with ID {request.AccountId} not found.");
        }

        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Account deleted: {AccountId}", request.AccountId);

        return Unit.Value;
    }
}
