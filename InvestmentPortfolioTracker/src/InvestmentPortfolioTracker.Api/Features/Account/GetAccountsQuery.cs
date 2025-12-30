// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioTracker.Api.Features.Account;

/// <summary>
/// Query to get all accounts.
/// </summary>
public record GetAccountsQuery : IRequest<List<AccountDto>>;

/// <summary>
/// Handler for GetAccountsQuery.
/// </summary>
public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, List<AccountDto>>
{
    private readonly IInvestmentPortfolioTrackerContext _context;
    private readonly ILogger<GetAccountsQueryHandler> _logger;

    public GetAccountsQueryHandler(
        IInvestmentPortfolioTrackerContext context,
        ILogger<GetAccountsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<AccountDto>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all accounts");

        var accounts = await _context.Accounts
            .OrderBy(a => a.Name)
            .ToListAsync(cancellationToken);

        _logger.LogInformation("Retrieved {Count} accounts", accounts.Count);

        return accounts.Select(a => a.ToDto()).ToList();
    }
}
