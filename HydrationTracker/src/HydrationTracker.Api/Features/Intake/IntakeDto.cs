// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HydrationTracker.Core;

namespace HydrationTracker.Api.Features.Intake;

public record IntakeDto(
    Guid IntakeId,
    Guid UserId,
    BeverageType BeverageType,
    decimal AmountMl,
    DateTime IntakeTime,
    string? Notes,
    DateTime CreatedAt);

public static class IntakeExtensions
{
    public static IntakeDto ToDto(this Core.Intake intake)
    {
        return new IntakeDto(
            intake.IntakeId,
            intake.UserId,
            intake.BeverageType,
            intake.AmountMl,
            intake.IntakeTime,
            intake.Notes,
            intake.CreatedAt);
    }
}
