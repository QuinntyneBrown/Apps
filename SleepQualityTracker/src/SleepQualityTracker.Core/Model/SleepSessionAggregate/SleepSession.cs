// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SleepQualityTracker.Core;

/// <summary>
/// Represents a sleep session or night of sleep.
/// </summary>
public class SleepSession
{
    /// <summary>
    /// Gets or sets the unique identifier for the sleep session.
    /// </summary>
    public Guid SleepSessionId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this sleep session.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the bedtime.
    /// </summary>
    public DateTime Bedtime { get; set; }

    /// <summary>
    /// Gets or sets the wake time.
    /// </summary>
    public DateTime WakeTime { get; set; }

    /// <summary>
    /// Gets or sets the total sleep duration in minutes.
    /// </summary>
    public int TotalSleepMinutes { get; set; }

    /// <summary>
    /// Gets or sets the sleep quality rating.
    /// </summary>
    public SleepQuality SleepQuality { get; set; }

    /// <summary>
    /// Gets or sets the number of times awakened during sleep.
    /// </summary>
    public int? TimesAwakened { get; set; }

    /// <summary>
    /// Gets or sets the deep sleep duration in minutes.
    /// </summary>
    public int? DeepSleepMinutes { get; set; }

    /// <summary>
    /// Gets or sets the REM sleep duration in minutes.
    /// </summary>
    public int? RemSleepMinutes { get; set; }

    /// <summary>
    /// Gets or sets the sleep efficiency percentage.
    /// </summary>
    public decimal? SleepEfficiency { get; set; }

    /// <summary>
    /// Gets or sets optional notes about the sleep session.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Calculates the total time in bed in minutes.
    /// </summary>
    /// <returns>The total time in bed.</returns>
    public int GetTimeInBed()
    {
        return (int)(WakeTime - Bedtime).TotalMinutes;
    }

    /// <summary>
    /// Checks if the sleep duration meets recommended 7-9 hours.
    /// </summary>
    /// <returns>True if meets recommendation; otherwise, false.</returns>
    public bool MeetsRecommendedDuration()
    {
        return TotalSleepMinutes >= 420 && TotalSleepMinutes <= 540; // 7-9 hours
    }

    /// <summary>
    /// Checks if the sleep quality is good or excellent.
    /// </summary>
    /// <returns>True if good quality; otherwise, false.</returns>
    public bool IsGoodQuality()
    {
        return SleepQuality == SleepQuality.Good || SleepQuality == SleepQuality.Excellent;
    }
}
