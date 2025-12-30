// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MedicationReminderSystem.Core;
using Microsoft.EntityFrameworkCore;

namespace MedicationReminderSystem.Api.Features.Refill;

/// <summary>
/// Query to get all refills.
/// </summary>
public record GetRefillsQuery : IRequest<List<RefillDto>>;

/// <summary>
/// Handler for GetRefillsQuery.
/// </summary>
public class GetRefillsQueryHandler : IRequestHandler<GetRefillsQuery, List<RefillDto>>
{
    private readonly IMedicationReminderSystemContext _context;
    private readonly ILogger<GetRefillsQueryHandler> _logger;

    public GetRefillsQueryHandler(
        IMedicationReminderSystemContext context,
        ILogger<GetRefillsQueryHandler> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<List<RefillDto>> Handle(GetRefillsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all refills");

        var refills = await _context.Refills
            .AsNoTracking()
            .OrderByDescending(r => r.RefillDate)
            .ToListAsync(cancellationToken);

        return refills.Select(r => r.ToDto()).ToList();
    }
}
