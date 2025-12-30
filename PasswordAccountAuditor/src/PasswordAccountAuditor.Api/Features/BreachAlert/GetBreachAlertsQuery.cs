// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PasswordAccountAuditor.Core;

namespace PasswordAccountAuditor.Api.Features.BreachAlert;

/// <summary>
/// Query to get all breach alerts.
/// </summary>
public record GetBreachAlertsQuery : IRequest<List<BreachAlertDto>>;

/// <summary>
/// Handler for GetBreachAlertsQuery.
/// </summary>
public class GetBreachAlertsQueryHandler : IRequestHandler<GetBreachAlertsQuery, List<BreachAlertDto>>
{
    private readonly IPasswordAccountAuditorContext _context;

    public GetBreachAlertsQueryHandler(IPasswordAccountAuditorContext context)
    {
        _context = context;
    }

    public async Task<List<BreachAlertDto>> Handle(GetBreachAlertsQuery request, CancellationToken cancellationToken)
    {
        var breachAlerts = await _context.BreachAlerts
            .AsNoTracking()
            .OrderByDescending(b => b.DetectedDate)
            .ToListAsync(cancellationToken);

        return breachAlerts.Select(b => b.ToDto()).ToList();
    }
}
