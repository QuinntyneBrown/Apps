// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using LetterToFutureSelf.Core;

namespace LetterToFutureSelf.Api;

/// <summary>
/// Data transfer object for Letter.
/// </summary>
public record LetterDto
{
    /// <summary>
    /// Gets or sets the letter ID.
    /// </summary>
    public Guid LetterId { get; init; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets or sets the subject.
    /// </summary>
    public string Subject { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the content.
    /// </summary>
    public string Content { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the written date.
    /// </summary>
    public DateTime WrittenDate { get; init; }

    /// <summary>
    /// Gets or sets the scheduled delivery date.
    /// </summary>
    public DateTime ScheduledDeliveryDate { get; init; }

    /// <summary>
    /// Gets or sets the actual delivery date.
    /// </summary>
    public DateTime? ActualDeliveryDate { get; init; }

    /// <summary>
    /// Gets or sets the delivery status.
    /// </summary>
    public DeliveryStatus DeliveryStatus { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether the letter has been read.
    /// </summary>
    public bool HasBeenRead { get; init; }

    /// <summary>
    /// Gets or sets the read date.
    /// </summary>
    public DateTime? ReadDate { get; init; }

    /// <summary>
    /// Gets or sets the created timestamp.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Gets or sets the updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether the letter is due for delivery.
    /// </summary>
    public bool IsDueForDelivery { get; init; }
}

/// <summary>
/// Extension methods for Letter.
/// </summary>
public static class LetterExtensions
{
    /// <summary>
    /// Converts a Letter to a DTO.
    /// </summary>
    /// <param name="letter">The letter.</param>
    /// <returns>The DTO.</returns>
    public static LetterDto ToDto(this Letter letter)
    {
        return new LetterDto
        {
            LetterId = letter.LetterId,
            UserId = letter.UserId,
            Subject = letter.Subject,
            Content = letter.Content,
            WrittenDate = letter.WrittenDate,
            ScheduledDeliveryDate = letter.ScheduledDeliveryDate,
            ActualDeliveryDate = letter.ActualDeliveryDate,
            DeliveryStatus = letter.DeliveryStatus,
            HasBeenRead = letter.HasBeenRead,
            ReadDate = letter.ReadDate,
            CreatedAt = letter.CreatedAt,
            UpdatedAt = letter.UpdatedAt,
            IsDueForDelivery = letter.IsDueForDelivery(),
        };
    }
}
