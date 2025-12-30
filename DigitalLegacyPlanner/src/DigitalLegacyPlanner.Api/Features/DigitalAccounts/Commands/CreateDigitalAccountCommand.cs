// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using MediatR;

namespace DigitalLegacyPlanner.Api.Features.DigitalAccounts.Commands;

/// <summary>
/// Command to create a new digital account.
/// </summary>
public class CreateDigitalAccountCommand : IRequest<DigitalAccountDto>
{
    public Guid UserId { get; set; }
    public AccountType AccountType { get; set; }
    public string AccountName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string? PasswordHint { get; set; }
    public string? Url { get; set; }
    public string? DesiredAction { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Handler for CreateDigitalAccountCommand.
/// </summary>
public class CreateDigitalAccountCommandHandler : IRequestHandler<CreateDigitalAccountCommand, DigitalAccountDto>
{
    private readonly IDigitalLegacyPlannerContext _context;

    public CreateDigitalAccountCommandHandler(IDigitalLegacyPlannerContext context)
    {
        _context = context;
    }

    public async Task<DigitalAccountDto> Handle(CreateDigitalAccountCommand request, CancellationToken cancellationToken)
    {
        var account = new DigitalAccount
        {
            DigitalAccountId = Guid.NewGuid(),
            UserId = request.UserId,
            AccountType = request.AccountType,
            AccountName = request.AccountName,
            Username = request.Username,
            PasswordHint = request.PasswordHint,
            Url = request.Url,
            DesiredAction = request.DesiredAction,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow
        };

        _context.Accounts.Add(account);
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
