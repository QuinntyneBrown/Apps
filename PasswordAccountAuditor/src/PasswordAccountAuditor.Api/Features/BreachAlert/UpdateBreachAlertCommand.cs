// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PasswordAccountAuditor.Core;

namespace PasswordAccountAuditor.Api.Features.BreachAlert;

/// <summary>
/// Command to update an existing breach alert.
/// </summary>
public record UpdateBreachAlertCommand : IRequest<BreachAlertDto>
{
    public Guid BreachAlertId { get; init; }
    public BreachSeverity Severity { get; init; }
    public AlertStatus Status { get; init; }
    public DateTime? BreachDate { get; init; }
    public string? Source { get; init; }
    public string Description { get; init; } = string.Empty;
    public string? DataCompromised { get; init; }
    public string? RecommendedActions { get; init; }
    public string? Notes { get; init; }
}

/// <summary>
/// Handler for UpdateBreachAlertCommand.
/// </summary>
public class UpdateBreachAlertCommandHandler : IRequestHandler<UpdateBreachAlertCommand, BreachAlertDto>
{
    private readonly IPasswordAccountAuditorContext _context;

    public UpdateBreachAlertCommandHandler(IPasswordAccountAuditorContext context)
    {
        _context = context;
    }

    public async Task<BreachAlertDto> Handle(UpdateBreachAlertCommand request, CancellationToken cancellationToken)
    {
        var breachAlert = await _context.BreachAlerts
            .FirstOrDefaultAsync(b => b.BreachAlertId == request.BreachAlertId, cancellationToken);

        if (breachAlert == null)
        {
            throw new InvalidOperationException($"BreachAlert with ID {request.BreachAlertId} not found.");
        }

        breachAlert.Severity = request.Severity;
        breachAlert.BreachDate = request.BreachDate;
        breachAlert.Source = request.Source;
        breachAlert.Description = request.Description;
        breachAlert.DataCompromised = request.DataCompromised;
        breachAlert.RecommendedActions = request.RecommendedActions;
        breachAlert.Notes = request.Notes;

        // Handle status changes with domain methods
        if (request.Status == AlertStatus.Acknowledged && breachAlert.Status != AlertStatus.Acknowledged)
        {
            breachAlert.Acknowledge();
        }
        else if (request.Status == AlertStatus.Resolved && breachAlert.Status != AlertStatus.Resolved)
        {
            breachAlert.Resolve();
        }
        else if (request.Status == AlertStatus.Dismissed && breachAlert.Status != AlertStatus.Dismissed)
        {
            breachAlert.Dismiss("Updated via API");
        }
        else
        {
            breachAlert.Status = request.Status;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return breachAlert.ToDto();
    }
}
