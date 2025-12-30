// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MedicationReminderSystem.Api.Features.Refill;

/// <summary>
/// Data transfer object for Refill.
/// </summary>
public record RefillDto
{
    public Guid RefillId { get; init; }
    public Guid UserId { get; init; }
    public Guid MedicationId { get; init; }
    public DateTime RefillDate { get; init; }
    public int Quantity { get; init; }
    public string? PharmacyName { get; init; }
    public decimal? Cost { get; init; }
    public DateTime? NextRefillDate { get; init; }
    public int? RefillsRemaining { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
}

/// <summary>
/// Extension methods for Refill entity.
/// </summary>
public static class RefillExtensions
{
    /// <summary>
    /// Converts a Refill entity to a RefillDto.
    /// </summary>
    public static RefillDto ToDto(this Core.Refill refill)
    {
        return new RefillDto
        {
            RefillId = refill.RefillId,
            UserId = refill.UserId,
            MedicationId = refill.MedicationId,
            RefillDate = refill.RefillDate,
            Quantity = refill.Quantity,
            PharmacyName = refill.PharmacyName,
            Cost = refill.Cost,
            NextRefillDate = refill.NextRefillDate,
            RefillsRemaining = refill.RefillsRemaining,
            Notes = refill.Notes,
            CreatedAt = refill.CreatedAt
        };
    }
}
