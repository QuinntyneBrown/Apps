// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PasswordAccountAuditor.Core;

namespace PasswordAccountAuditor.Api.Features.BreachAlert;

/// <summary>
/// Query to get a breach alert by ID.
/// </summary>
public record GetBreachAlertByIdQuery(Guid BreachAlertId) : IRequest<BreachAlertDto?>;

/// <summary>
/// Handler for GetBreachAlertByIdQuery.
/// </summary>
public class GetBreachAlertByIdQueryHandler : IRequestHandler<GetBreachAlertByIdQuery, BreachAlertDto?>
{
    private readonly IPasswordAccountAuditorContext _context;

    public GetBreachAlertByIdQueryHandler(IPasswordAccountAuditorContext context)
    {
        _context = context;
    }

    public async Task<BreachAlertDto?> Handle(GetBreachAlertByIdQuery request, CancellationToken cancellationToken)
    {
        var breachAlert = await _context.BreachAlerts
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.BreachAlertId == request.BreachAlertId, cancellationToken);

        return breachAlert?.ToDto();
    }
}
