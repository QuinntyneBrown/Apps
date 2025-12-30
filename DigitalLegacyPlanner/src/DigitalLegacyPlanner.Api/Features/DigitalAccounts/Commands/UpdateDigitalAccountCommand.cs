// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DigitalLegacyPlanner.Api.Features.DigitalAccounts.Commands;

/// <summary>
/// Command to update an existing digital account.
/// </summary>
public class UpdateDigitalAccountCommand : IRequest<DigitalAccountDto?>
{
    public Guid DigitalAccountId { get; set; }
    public AccountType AccountType { get; set; }
    public string AccountName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string? PasswordHint { get; set; }
    public string? Url { get; set; }
    public string? DesiredAction { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Handler for UpdateDigitalAccountCommand.
/// </summary>
public class UpdateDigitalAccountCommandHandler : IRequestHandler<UpdateDigitalAccountCommand, DigitalAccountDto?>
{
    private readonly IDigitalLegacyPlannerContext _context;

    public UpdateDigitalAccountCommandHandler(IDigitalLegacyPlannerContext context)
    {
        _context = context;
    }

    public async Task<DigitalAccountDto?> Handle(UpdateDigitalAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.DigitalAccountId == request.DigitalAccountId, cancellationToken);

        if (account == null)
        {
            return null;
        }

        account.AccountType = request.AccountType;
        account.AccountName = request.AccountName;
        account.Username = request.Username;
        account.PasswordHint = request.PasswordHint;
        account.Url = request.Url;
        account.DesiredAction = request.DesiredAction;
        account.Notes = request.Notes;
        account.UpdateAccount();

        await _context.SaveChangesAsync(cancellationToken);

        return new DigitalAccountDto
        {
            DigitalAccountId = account.DigitalAccountId,
            UserId = account.UserId,
            AccountType = account.AccountType,
            AccountName = account.AccountName,
            Username = account.Username,
            PasswordHint = account.PasswordHint,
            Url = account.Url,
            DesiredAction = account.DesiredAction,
            Notes = account.Notes,
            CreatedAt = account.CreatedAt,
            LastUpdatedAt = account.LastUpdatedAt
        };
    }
}
