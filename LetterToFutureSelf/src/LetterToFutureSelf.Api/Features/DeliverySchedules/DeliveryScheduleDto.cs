// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using LetterToFutureSelf.Core;

namespace LetterToFutureSelf.Api;

/// <summary>
/// Data transfer object for DeliverySchedule.
/// </summary>
public record DeliveryScheduleDto
{
    /// <summary>
    /// Gets or sets the delivery schedule ID.
    /// </summary>
    public Guid DeliveryScheduleId { get; init; }

    /// <summary>
    /// Gets or sets the letter ID.
    /// </summary>
    public Guid LetterId { get; init; }

    /// <summary>
    /// Gets or sets the scheduled date and time.
    /// </summary>
    public DateTime ScheduledDateTime { get; init; }

    /// <summary>
    /// Gets or sets the delivery method.
    /// </summary>
    public string DeliveryMethod { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the recipient contact.
    /// </summary>
    public string? RecipientContact { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether the schedule is active.
    /// </summary>
    public bool IsActive { get; init; }

    /// <summary>
    /// Gets or sets the created timestamp.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Gets or sets the updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; init; }
}

/// <summary>
/// Extension methods for DeliverySchedule.
/// </summary>
public static class DeliveryScheduleExtensions
{
    /// <summary>
    /// Converts a DeliverySchedule to a DTO.
    /// </summary>
    /// <param name="deliverySchedule">The delivery schedule.</param>
    /// <returns>The DTO.</returns>
    public static DeliveryScheduleDto ToDto(this DeliverySchedule deliverySchedule)
    {
        return new DeliveryScheduleDto
        {
            DeliveryScheduleId = deliverySchedule.DeliveryScheduleId,
            LetterId = deliverySchedule.LetterId,
            ScheduledDateTime = deliverySchedule.ScheduledDateTime,
            DeliveryMethod = deliverySchedule.DeliveryMethod,
            RecipientContact = deliverySchedule.RecipientContact,
            IsActive = deliverySchedule.IsActive,
            CreatedAt = deliverySchedule.CreatedAt,
            UpdatedAt = deliverySchedule.UpdatedAt,
        };
    }
}
