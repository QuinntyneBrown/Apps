// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FuelEconomyTracker.Api.Features.EfficiencyReports.Commands;

/// <summary>
/// Command to delete an efficiency report.
/// </summary>
public class DeleteEfficiencyReport : IRequest<Unit>
{
    public Guid EfficiencyReportId { get; set; }
}

/// <summary>
/// Handler for DeleteEfficiencyReport command.
/// </summary>
public class DeleteEfficiencyReportHandler : IRequestHandler<DeleteEfficiencyReport, Unit>
{
    private readonly IFuelEconomyTrackerContext _context;

    public DeleteEfficiencyReportHandler(IFuelEconomyTrackerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteEfficiencyReport request, CancellationToken cancellationToken)
    {
        var report = await _context.EfficiencyReports
            .FirstOrDefaultAsync(r => r.EfficiencyReportId == request.EfficiencyReportId, cancellationToken);

        if (report == null)
        {
            throw new KeyNotFoundException($"EfficiencyReport with ID {request.EfficiencyReportId} not found.");
        }

        _context.EfficiencyReports.Remove(report);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
