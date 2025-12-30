// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MedicationReminderSystem.Core;
using Microsoft.EntityFrameworkCore;

namespace MedicationReminderSystem.Api.Features.Medication;

/// <summary>
/// Query to get a medication by ID.
/// </summary>
public record GetMedicationByIdQuery(Guid MedicationId) : IRequest<MedicationDto?>;

/// <summary>
/// Handler for GetMedicationByIdQuery.
/// </summary>
public class GetMedicationByIdQueryHandler : IRequestHandler<GetMedicationByIdQuery, MedicationDto?>
{
    private readonly IMedicationReminderSystemContext _context;
    private readonly ILogger<GetMedicationByIdQueryHandler> _logger;

    public GetMedicationByIdQueryHandler(
        IMedicationReminderSystemContext context,
        ILogger<GetMedicationByIdQueryHandler> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<MedicationDto?> Handle(GetMedicationByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting medication by ID: {MedicationId}", request.MedicationId);

        var medication = await _context.Medications
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MedicationId == request.MedicationId, cancellationToken);

        return medication?.ToDto();
    }
}
