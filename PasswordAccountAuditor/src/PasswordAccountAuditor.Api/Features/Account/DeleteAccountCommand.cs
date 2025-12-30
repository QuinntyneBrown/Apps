// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PasswordAccountAuditor.Core;

namespace PasswordAccountAuditor.Api.Features.Account;

/// <summary>
/// Command to delete an account.
/// </summary>
public record DeleteAccountCommand(Guid AccountId) : IRequest<Unit>;

/// <summary>
/// Handler for DeleteAccountCommand.
/// </summary>
public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Unit>
{
    private readonly IPasswordAccountAuditorContext _context;

    public DeleteAccountCommandHandler(IPasswordAccountAuditorContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.AccountId == request.AccountId, cancellationToken);

        if (account == null)
        {
            throw new InvalidOperationException($"Account with ID {request.AccountId} not found.");
        }

        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
