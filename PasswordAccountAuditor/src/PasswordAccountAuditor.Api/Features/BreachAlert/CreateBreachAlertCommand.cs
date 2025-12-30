// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PasswordAccountAuditor.Core;

namespace PasswordAccountAuditor.Api.Features.BreachAlert;

/// <summary>
/// Command to create a new breach alert.
/// </summary>
public record CreateBreachAlertCommand : IRequest<BreachAlertDto>
{
    public Guid AccountId { get; init; }
    public BreachSeverity Severity { get; init; }
    public DateTime? BreachDate { get; init; }
    public string? Source { get; init; }
    public string Description { get; init; } = string.Empty;
    public string? DataCompromised { get; init; }
    public string? RecommendedActions { get; init; }
    public string? Notes { get; init; }
}

/// <summary>
/// Handler for CreateBreachAlertCommand.
/// </summary>
public class CreateBreachAlertCommandHandler : IRequestHandler<CreateBreachAlertCommand, BreachAlertDto>
{
    private readonly IPasswordAccountAuditorContext _context;

    public CreateBreachAlertCommandHandler(IPasswordAccountAuditorContext context)
    {
        _context = context;
    }

    public async Task<BreachAlertDto> Handle(CreateBreachAlertCommand request, CancellationToken cancellationToken)
    {
        var breachAlert = new Core.BreachAlert
        {
            BreachAlertId = Guid.NewGuid(),
            AccountId = request.AccountId,
            Severity = request.Severity,
            Status = AlertStatus.New,
            DetectedDate = DateTime.UtcNow,
            BreachDate = request.BreachDate,
            Source = request.Source,
            Description = request.Description,
            DataCompromised = request.DataCompromised,
            RecommendedActions = request.RecommendedActions,
            Notes = request.Notes
        };

        _context.BreachAlerts.Add(breachAlert);
        await _context.SaveChangesAsync(cancellationToken);

        return breachAlert.ToDto();
    }
}
