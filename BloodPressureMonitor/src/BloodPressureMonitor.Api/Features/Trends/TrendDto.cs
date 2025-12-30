// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BloodPressureMonitor.Core;

namespace BloodPressureMonitor.Api;

/// <summary>
/// Data transfer object for Trend.
/// </summary>
public record TrendDto
{
    /// <summary>
    /// Gets or sets the trend ID.
    /// </summary>
    public Guid TrendId { get; init; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    public DateTime StartDate { get; init; }

    /// <summary>
    /// Gets or sets the end date.
    /// </summary>
    public DateTime EndDate { get; init; }

    /// <summary>
    /// Gets or sets the average systolic pressure.
    /// </summary>
    public decimal AverageSystolic { get; init; }

    /// <summary>
    /// Gets or sets the average diastolic pressure.
    /// </summary>
    public decimal AverageDiastolic { get; init; }

    /// <summary>
    /// Gets or sets the highest systolic reading.
    /// </summary>
    public int HighestSystolic { get; init; }

    /// <summary>
    /// Gets or sets the highest diastolic reading.
    /// </summary>
    public int HighestDiastolic { get; init; }

    /// <summary>
    /// Gets or sets the lowest systolic reading.
    /// </summary>
    public int LowestSystolic { get; init; }

    /// <summary>
    /// Gets or sets the lowest diastolic reading.
    /// </summary>
    public int LowestDiastolic { get; init; }

    /// <summary>
    /// Gets or sets the reading count.
    /// </summary>
    public int ReadingCount { get; init; }

    /// <summary>
    /// Gets or sets the trend direction.
    /// </summary>
    public string TrendDirection { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the insights.
    /// </summary>
    public string? Insights { get; init; }

    /// <summary>
    /// Gets or sets the created timestamp.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Gets the period duration in days.
    /// </summary>
    public int PeriodDuration { get; init; }

    /// <summary>
    /// Gets a value indicating whether the trend is improving.
    /// </summary>
    public bool IsImproving { get; init; }
}

/// <summary>
/// Extension methods for Trend.
/// </summary>
public static class TrendExtensions
{
    /// <summary>
    /// Converts a Trend to a DTO.
    /// </summary>
    /// <param name="trend">The trend.</param>
    /// <returns>The DTO.</returns>
    public static TrendDto ToDto(this Trend trend)
    {
        return new TrendDto
        {
            TrendId = trend.TrendId,
            UserId = trend.UserId,
            StartDate = trend.StartDate,
            EndDate = trend.EndDate,
            AverageSystolic = trend.AverageSystolic,
            AverageDiastolic = trend.AverageDiastolic,
            HighestSystolic = trend.HighestSystolic,
            HighestDiastolic = trend.HighestDiastolic,
            LowestSystolic = trend.LowestSystolic,
            LowestDiastolic = trend.LowestDiastolic,
            ReadingCount = trend.ReadingCount,
            TrendDirection = trend.TrendDirection,
            Insights = trend.Insights,
            CreatedAt = trend.CreatedAt,
            PeriodDuration = trend.GetPeriodDuration(),
            IsImproving = trend.IsImproving(),
        };
    }
}
