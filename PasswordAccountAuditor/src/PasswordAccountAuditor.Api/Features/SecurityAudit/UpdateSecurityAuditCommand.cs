// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PasswordAccountAuditor.Core;

namespace PasswordAccountAuditor.Api.Features.SecurityAudit;

/// <summary>
/// Command to update an existing security audit.
/// </summary>
public record UpdateSecurityAuditCommand : IRequest<SecurityAuditDto>
{
    public Guid SecurityAuditId { get; init; }
    public AuditType AuditType { get; init; }
    public AuditStatus Status { get; init; }
    public string? Findings { get; init; }
    public string? Recommendations { get; init; }
    public int SecurityScore { get; init; }
    public string? Notes { get; init; }
}

/// <summary>
/// Handler for UpdateSecurityAuditCommand.
/// </summary>
public class UpdateSecurityAuditCommandHandler : IRequestHandler<UpdateSecurityAuditCommand, SecurityAuditDto>
{
    private readonly IPasswordAccountAuditorContext _context;

    public UpdateSecurityAuditCommandHandler(IPasswordAccountAuditorContext context)
    {
        _context = context;
    }

    public async Task<SecurityAuditDto> Handle(UpdateSecurityAuditCommand request, CancellationToken cancellationToken)
    {
        var securityAudit = await _context.SecurityAudits
            .FirstOrDefaultAsync(s => s.SecurityAuditId == request.SecurityAuditId, cancellationToken);

        if (securityAudit == null)
        {
            throw new InvalidOperationException($"SecurityAudit with ID {request.SecurityAuditId} not found.");
        }

        securityAudit.AuditType = request.AuditType;
        securityAudit.Status = request.Status;
        securityAudit.Findings = request.Findings;
        securityAudit.Recommendations = request.Recommendations;
        securityAudit.SecurityScore = Math.Clamp(request.SecurityScore, 0, 100);
        securityAudit.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return securityAudit.ToDto();
    }
}
