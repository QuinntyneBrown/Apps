// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GolfScoreTracker.Api.Features.Courses;

public class CourseDto
{
    public Guid CourseId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Location { get; set; }
    public int NumberOfHoles { get; set; }
    public int TotalPar { get; set; }
    public decimal? CourseRating { get; set; }
    public int? SlopeRating { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}
