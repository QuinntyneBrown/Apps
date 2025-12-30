// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PasswordAccountAuditor.Core;

namespace PasswordAccountAuditor.Api.Features.SecurityAudit;

/// <summary>
/// Command to create a new security audit.
/// </summary>
public record CreateSecurityAuditCommand : IRequest<SecurityAuditDto>
{
    public Guid AccountId { get; init; }
    public AuditType AuditType { get; init; }
    public string? Findings { get; init; }
    public string? Recommendations { get; init; }
    public int SecurityScore { get; init; }
    public string? Notes { get; init; }
}

/// <summary>
/// Handler for CreateSecurityAuditCommand.
/// </summary>
public class CreateSecurityAuditCommandHandler : IRequestHandler<CreateSecurityAuditCommand, SecurityAuditDto>
{
    private readonly IPasswordAccountAuditorContext _context;

    public CreateSecurityAuditCommandHandler(IPasswordAccountAuditorContext context)
    {
        _context = context;
    }

    public async Task<SecurityAuditDto> Handle(CreateSecurityAuditCommand request, CancellationToken cancellationToken)
    {
        var securityAudit = new Core.SecurityAudit
        {
            SecurityAuditId = Guid.NewGuid(),
            AccountId = request.AccountId,
            AuditType = request.AuditType,
            Status = AuditStatus.Pending,
            AuditDate = DateTime.UtcNow,
            Findings = request.Findings,
            Recommendations = request.Recommendations,
            SecurityScore = Math.Clamp(request.SecurityScore, 0, 100),
            Notes = request.Notes
        };

        _context.SecurityAudits.Add(securityAudit);
        await _context.SaveChangesAsync(cancellationToken);

        return securityAudit.ToDto();
    }
}
