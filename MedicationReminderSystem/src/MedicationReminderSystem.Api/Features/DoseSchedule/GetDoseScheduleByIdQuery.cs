// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MedicationReminderSystem.Core;
using Microsoft.EntityFrameworkCore;

namespace MedicationReminderSystem.Api.Features.DoseSchedule;

/// <summary>
/// Query to get a dose schedule by ID.
/// </summary>
public record GetDoseScheduleByIdQuery(Guid DoseScheduleId) : IRequest<DoseScheduleDto?>;

/// <summary>
/// Handler for GetDoseScheduleByIdQuery.
/// </summary>
public class GetDoseScheduleByIdQueryHandler : IRequestHandler<GetDoseScheduleByIdQuery, DoseScheduleDto?>
{
    private readonly IMedicationReminderSystemContext _context;
    private readonly ILogger<GetDoseScheduleByIdQueryHandler> _logger;

    public GetDoseScheduleByIdQueryHandler(
        IMedicationReminderSystemContext context,
        ILogger<GetDoseScheduleByIdQueryHandler> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<DoseScheduleDto?> Handle(GetDoseScheduleByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting dose schedule by ID: {DoseScheduleId}", request.DoseScheduleId);

        var doseSchedule = await _context.DoseSchedules
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.DoseScheduleId == request.DoseScheduleId, cancellationToken);

        return doseSchedule?.ToDto();
    }
}
