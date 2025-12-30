// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioTracker.Api.Features.Account;

/// <summary>
/// Query to get an account by ID.
/// </summary>
public record GetAccountByIdQuery(Guid AccountId) : IRequest<AccountDto?>;

/// <summary>
/// Handler for GetAccountByIdQuery.
/// </summary>
public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountDto?>
{
    private readonly IInvestmentPortfolioTrackerContext _context;
    private readonly ILogger<GetAccountByIdQueryHandler> _logger;

    public GetAccountByIdQueryHandler(
        IInvestmentPortfolioTrackerContext context,
        ILogger<GetAccountByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<AccountDto?> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting account: {AccountId}", request.AccountId);

        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.AccountId == request.AccountId, cancellationToken);

        if (account == null)
        {
            _logger.LogWarning("Account not found: {AccountId}", request.AccountId);
            return null;
        }

        return account.ToDto();
    }
}
