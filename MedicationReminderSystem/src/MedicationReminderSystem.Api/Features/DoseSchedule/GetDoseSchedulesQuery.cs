// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MedicationReminderSystem.Core;
using Microsoft.EntityFrameworkCore;

namespace MedicationReminderSystem.Api.Features.DoseSchedule;

/// <summary>
/// Query to get all dose schedules.
/// </summary>
public record GetDoseSchedulesQuery : IRequest<List<DoseScheduleDto>>;

/// <summary>
/// Handler for GetDoseSchedulesQuery.
/// </summary>
public class GetDoseSchedulesQueryHandler : IRequestHandler<GetDoseSchedulesQuery, List<DoseScheduleDto>>
{
    private readonly IMedicationReminderSystemContext _context;
    private readonly ILogger<GetDoseSchedulesQueryHandler> _logger;

    public GetDoseSchedulesQueryHandler(
        IMedicationReminderSystemContext context,
        ILogger<GetDoseSchedulesQueryHandler> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<List<DoseScheduleDto>> Handle(GetDoseSchedulesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all dose schedules");

        var doseSchedules = await _context.DoseSchedules
            .AsNoTracking()
            .OrderBy(d => d.ScheduledTime)
            .ToListAsync(cancellationToken);

        return doseSchedules.Select(d => d.ToDto()).ToList();
    }
}
