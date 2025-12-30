// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MedicationReminderSystem.Core;
using Microsoft.EntityFrameworkCore;

namespace MedicationReminderSystem.Api.Features.Refill;

/// <summary>
/// Query to get a refill by ID.
/// </summary>
public record GetRefillByIdQuery(Guid RefillId) : IRequest<RefillDto?>;

/// <summary>
/// Handler for GetRefillByIdQuery.
/// </summary>
public class GetRefillByIdQueryHandler : IRequestHandler<GetRefillByIdQuery, RefillDto?>
{
    private readonly IMedicationReminderSystemContext _context;
    private readonly ILogger<GetRefillByIdQueryHandler> _logger;

    public GetRefillByIdQueryHandler(
        IMedicationReminderSystemContext context,
        ILogger<GetRefillByIdQueryHandler> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<RefillDto?> Handle(GetRefillByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting refill by ID: {RefillId}", request.RefillId);

        var refill = await _context.Refills
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.RefillId == request.RefillId, cancellationToken);

        return refill?.ToDto();
    }
}
