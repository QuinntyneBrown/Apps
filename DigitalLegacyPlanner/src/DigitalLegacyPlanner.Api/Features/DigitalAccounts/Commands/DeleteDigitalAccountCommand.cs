// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DigitalLegacyPlanner.Api.Features.DigitalAccounts.Commands;

/// <summary>
/// Command to delete a digital account.
/// </summary>
public class DeleteDigitalAccountCommand : IRequest<bool>
{
    public Guid DigitalAccountId { get; set; }
}

/// <summary>
/// Handler for DeleteDigitalAccountCommand.
/// </summary>
public class DeleteDigitalAccountCommandHandler : IRequestHandler<DeleteDigitalAccountCommand, bool>
{
    private readonly IDigitalLegacyPlannerContext _context;

    public DeleteDigitalAccountCommandHandler(IDigitalLegacyPlannerContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteDigitalAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.DigitalAccountId == request.DigitalAccountId, cancellationToken);

        if (account == null)
        {
            return false;
        }

        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
