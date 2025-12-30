// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PasswordAccountAuditor.Core;

namespace PasswordAccountAuditor.Api.Features.BreachAlert;

/// <summary>
/// Command to delete a breach alert.
/// </summary>
public record DeleteBreachAlertCommand(Guid BreachAlertId) : IRequest<Unit>;

/// <summary>
/// Handler for DeleteBreachAlertCommand.
/// </summary>
public class DeleteBreachAlertCommandHandler : IRequestHandler<DeleteBreachAlertCommand, Unit>
{
    private readonly IPasswordAccountAuditorContext _context;

    public DeleteBreachAlertCommandHandler(IPasswordAccountAuditorContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteBreachAlertCommand request, CancellationToken cancellationToken)
    {
        var breachAlert = await _context.BreachAlerts
            .FirstOrDefaultAsync(b => b.BreachAlertId == request.BreachAlertId, cancellationToken);

        if (breachAlert == null)
        {
            throw new InvalidOperationException($"BreachAlert with ID {request.BreachAlertId} not found.");
        }

        _context.BreachAlerts.Remove(breachAlert);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
