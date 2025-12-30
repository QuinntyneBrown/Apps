// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioTracker.Api.Features.Account;

/// <summary>
/// Command to create a new account.
/// </summary>
public record CreateAccountCommand : IRequest<AccountDto>
{
    public string Name { get; set; } = string.Empty;
    public AccountType AccountType { get; set; }
    public string Institution { get; set; } = string.Empty;
    public string? AccountNumber { get; set; }
    public decimal CurrentBalance { get; set; }
    public DateTime OpenedDate { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Handler for CreateAccountCommand.
/// </summary>
public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountDto>
{
    private readonly IInvestmentPortfolioTrackerContext _context;
    private readonly ILogger<CreateAccountCommandHandler> _logger;

    public CreateAccountCommandHandler(
        IInvestmentPortfolioTrackerContext context,
        ILogger<CreateAccountCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<AccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new account: {Name}", request.Name);

        var account = new Core.Account
        {
            AccountId = Guid.NewGuid(),
            Name = request.Name,
            AccountType = request.AccountType,
            Institution = request.Institution,
            AccountNumber = request.AccountNumber,
            CurrentBalance = request.CurrentBalance,
            IsActive = true,
            OpenedDate = request.OpenedDate,
            Notes = request.Notes
        };

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Account created with ID: {AccountId}", account.AccountId);

        return account.ToDto();
    }
}
