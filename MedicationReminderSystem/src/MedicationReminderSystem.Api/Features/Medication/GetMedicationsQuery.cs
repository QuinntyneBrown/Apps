// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MedicationReminderSystem.Core;
using Microsoft.EntityFrameworkCore;

namespace MedicationReminderSystem.Api.Features.Medication;

/// <summary>
/// Query to get all medications.
/// </summary>
public record GetMedicationsQuery : IRequest<List<MedicationDto>>;

/// <summary>
/// Handler for GetMedicationsQuery.
/// </summary>
public class GetMedicationsQueryHandler : IRequestHandler<GetMedicationsQuery, List<MedicationDto>>
{
    private readonly IMedicationReminderSystemContext _context;
    private readonly ILogger<GetMedicationsQueryHandler> _logger;

    public GetMedicationsQueryHandler(
        IMedicationReminderSystemContext context,
        ILogger<GetMedicationsQueryHandler> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<List<MedicationDto>> Handle(GetMedicationsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all medications");

        var medications = await _context.Medications
            .AsNoTracking()
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync(cancellationToken);

        return medications.Select(m => m.ToDto()).ToList();
    }
}
