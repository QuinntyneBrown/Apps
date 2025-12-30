// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PetCareManager.Core;

public record VaccinationRecordedEvent
{
    public Guid VaccinationId { get; init; }
    public Guid PetId { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTime DateAdministered { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
