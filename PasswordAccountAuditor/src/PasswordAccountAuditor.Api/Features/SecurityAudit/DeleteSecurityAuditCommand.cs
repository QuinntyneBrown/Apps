// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PasswordAccountAuditor.Core;

namespace PasswordAccountAuditor.Api.Features.SecurityAudit;

/// <summary>
/// Command to delete a security audit.
/// </summary>
public record DeleteSecurityAuditCommand(Guid SecurityAuditId) : IRequest<Unit>;

/// <summary>
/// Handler for DeleteSecurityAuditCommand.
/// </summary>
public class DeleteSecurityAuditCommandHandler : IRequestHandler<DeleteSecurityAuditCommand, Unit>
{
    private readonly IPasswordAccountAuditorContext _context;

    public DeleteSecurityAuditCommandHandler(IPasswordAccountAuditorContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteSecurityAuditCommand request, CancellationToken cancellationToken)
    {
        var securityAudit = await _context.SecurityAudits
            .FirstOrDefaultAsync(s => s.SecurityAuditId == request.SecurityAuditId, cancellationToken);

        if (securityAudit == null)
        {
            throw new InvalidOperationException($"SecurityAudit with ID {request.SecurityAuditId} not found.");
        }

        _context.SecurityAudits.Remove(securityAudit);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
