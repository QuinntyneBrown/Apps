// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GolfScoreTracker.Core;

public class Course
{
    public Guid CourseId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Location { get; set; }
    public int NumberOfHoles { get; set; } = 18;
    public int TotalPar { get; set; }
    public decimal? CourseRating { get; set; }
    public int? SlopeRating { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Round> Rounds { get; set; } = new List<Round>();
    
    public bool IsFullCourse()
    {
        return NumberOfHoles == 18;
    }
}
