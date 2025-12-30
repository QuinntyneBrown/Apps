// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PasswordAccountAuditor.Core;

namespace PasswordAccountAuditor.Api.Features.SecurityAudit;

/// <summary>
/// Query to get all security audits.
/// </summary>
public record GetSecurityAuditsQuery : IRequest<List<SecurityAuditDto>>;

/// <summary>
/// Handler for GetSecurityAuditsQuery.
/// </summary>
public class GetSecurityAuditsQueryHandler : IRequestHandler<GetSecurityAuditsQuery, List<SecurityAuditDto>>
{
    private readonly IPasswordAccountAuditorContext _context;

    public GetSecurityAuditsQueryHandler(IPasswordAccountAuditorContext context)
    {
        _context = context;
    }

    public async Task<List<SecurityAuditDto>> Handle(GetSecurityAuditsQuery request, CancellationToken cancellationToken)
    {
        var securityAudits = await _context.SecurityAudits
            .AsNoTracking()
            .OrderByDescending(s => s.AuditDate)
            .ToListAsync(cancellationToken);

        return securityAudits.Select(s => s.ToDto()).ToList();
    }
}
