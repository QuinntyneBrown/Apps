// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PetCareManager.Core;

public record PetAddedEvent
{
    public Guid PetId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public PetType PetType { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
