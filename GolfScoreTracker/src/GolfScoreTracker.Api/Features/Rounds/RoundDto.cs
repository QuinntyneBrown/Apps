// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GolfScoreTracker.Api.Features.Rounds;

public class RoundDto
{
    public Guid RoundId { get; set; }
    public Guid UserId { get; set; }
    public Guid CourseId { get; set; }
    public DateTime PlayedDate { get; set; }
    public int TotalScore { get; set; }
    public int TotalPar { get; set; }
    public string? Weather { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CourseName { get; set; }
}
