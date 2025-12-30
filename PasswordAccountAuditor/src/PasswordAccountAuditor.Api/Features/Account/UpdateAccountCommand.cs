// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PasswordAccountAuditor.Core;

namespace PasswordAccountAuditor.Api.Features.Account;

/// <summary>
/// Command to update an existing account.
/// </summary>
public record UpdateAccountCommand : IRequest<AccountDto>
{
    public Guid AccountId { get; init; }
    public string AccountName { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public string? WebsiteUrl { get; init; }
    public AccountCategory Category { get; init; }
    public bool HasTwoFactorAuth { get; init; }
    public DateTime? LastPasswordChange { get; init; }
    public string? Notes { get; init; }
    public bool IsActive { get; init; }
}

/// <summary>
/// Handler for UpdateAccountCommand.
/// </summary>
public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, AccountDto>
{
    private readonly IPasswordAccountAuditorContext _context;

    public UpdateAccountCommandHandler(IPasswordAccountAuditorContext context)
    {
        _context = context;
    }

    public async Task<AccountDto> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.AccountId == request.AccountId, cancellationToken);

        if (account == null)
        {
            throw new InvalidOperationException($"Account with ID {request.AccountId} not found.");
        }

        var hadTwoFactor = account.HasTwoFactorAuth;

        account.AccountName = request.AccountName;
        account.Username = request.Username;
        account.WebsiteUrl = request.WebsiteUrl;
        account.Category = request.Category;
        account.LastPasswordChange = request.LastPasswordChange;
        account.Notes = request.Notes;
        account.IsActive = request.IsActive;

        // Handle two-factor auth changes
        if (request.HasTwoFactorAuth && !hadTwoFactor)
        {
            account.EnableTwoFactorAuth();
        }
        else if (!request.HasTwoFactorAuth && hadTwoFactor)
        {
            account.DisableTwoFactorAuth();
        }

        await _context.SaveChangesAsync(cancellationToken);

        return account.ToDto();
    }
}
