// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PasswordAccountAuditor.Core;

namespace PasswordAccountAuditor.Api.Features.BreachAlert;

/// <summary>
/// Data transfer object for BreachAlert.
/// </summary>
public record BreachAlertDto
{
    public Guid BreachAlertId { get; init; }
    public Guid AccountId { get; init; }
    public BreachSeverity Severity { get; init; }
    public AlertStatus Status { get; init; }
    public DateTime DetectedDate { get; init; }
    public DateTime? BreachDate { get; init; }
    public string? Source { get; init; }
    public string Description { get; init; } = string.Empty;
    public string? DataCompromised { get; init; }
    public string? RecommendedActions { get; init; }
    public DateTime? AcknowledgedAt { get; init; }
    public DateTime? ResolvedAt { get; init; }
    public string? Notes { get; init; }
}

/// <summary>
/// Extension methods for mapping BreachAlert to BreachAlertDto.
/// </summary>
public static class BreachAlertExtensions
{
    /// <summary>
    /// Converts a BreachAlert entity to a BreachAlertDto.
    /// </summary>
    /// <param name="breachAlert">The breach alert entity.</param>
    /// <returns>The breach alert DTO.</returns>
    public static BreachAlertDto ToDto(this Core.BreachAlert breachAlert)
    {
        return new BreachAlertDto
        {
            BreachAlertId = breachAlert.BreachAlertId,
            AccountId = breachAlert.AccountId,
            Severity = breachAlert.Severity,
            Status = breachAlert.Status,
            DetectedDate = breachAlert.DetectedDate,
            BreachDate = breachAlert.BreachDate,
            Source = breachAlert.Source,
            Description = breachAlert.Description,
            DataCompromised = breachAlert.DataCompromised,
            RecommendedActions = breachAlert.RecommendedActions,
            AcknowledgedAt = breachAlert.AcknowledgedAt,
            ResolvedAt = breachAlert.ResolvedAt,
            Notes = breachAlert.Notes
        };
    }
}
