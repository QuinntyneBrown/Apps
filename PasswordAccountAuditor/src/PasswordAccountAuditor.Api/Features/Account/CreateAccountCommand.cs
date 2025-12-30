// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PasswordAccountAuditor.Core;

namespace PasswordAccountAuditor.Api.Features.Account;

/// <summary>
/// Command to create a new account.
/// </summary>
public record CreateAccountCommand : IRequest<AccountDto>
{
    public Guid UserId { get; init; }
    public string AccountName { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public string? WebsiteUrl { get; init; }
    public AccountCategory Category { get; init; }
    public bool HasTwoFactorAuth { get; init; }
    public DateTime? LastPasswordChange { get; init; }
    public string? Notes { get; init; }
}

/// <summary>
/// Handler for CreateAccountCommand.
/// </summary>
public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountDto>
{
    private readonly IPasswordAccountAuditorContext _context;

    public CreateAccountCommandHandler(IPasswordAccountAuditorContext context)
    {
        _context = context;
    }

    public async Task<AccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = new Core.Account
        {
            AccountId = Guid.NewGuid(),
            UserId = request.UserId,
            AccountName = request.AccountName,
            Username = request.Username,
            WebsiteUrl = request.WebsiteUrl,
            Category = request.Category,
            HasTwoFactorAuth = request.HasTwoFactorAuth,
            LastPasswordChange = request.LastPasswordChange,
            Notes = request.Notes,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        if (request.HasTwoFactorAuth)
        {
            account.EnableTwoFactorAuth();
        }

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync(cancellationToken);

        return account.ToDto();
    }
}
