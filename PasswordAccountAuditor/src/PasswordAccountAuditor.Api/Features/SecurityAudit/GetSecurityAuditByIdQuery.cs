// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PasswordAccountAuditor.Core;

namespace PasswordAccountAuditor.Api.Features.SecurityAudit;

/// <summary>
/// Query to get a security audit by ID.
/// </summary>
public record GetSecurityAuditByIdQuery(Guid SecurityAuditId) : IRequest<SecurityAuditDto?>;

/// <summary>
/// Handler for GetSecurityAuditByIdQuery.
/// </summary>
public class GetSecurityAuditByIdQueryHandler : IRequestHandler<GetSecurityAuditByIdQuery, SecurityAuditDto?>
{
    private readonly IPasswordAccountAuditorContext _context;

    public GetSecurityAuditByIdQueryHandler(IPasswordAccountAuditorContext context)
    {
        _context = context;
    }

    public async Task<SecurityAuditDto?> Handle(GetSecurityAuditByIdQuery request, CancellationToken cancellationToken)
    {
        var securityAudit = await _context.SecurityAudits
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.SecurityAuditId == request.SecurityAuditId, cancellationToken);

        return securityAudit?.ToDto();
    }
}
