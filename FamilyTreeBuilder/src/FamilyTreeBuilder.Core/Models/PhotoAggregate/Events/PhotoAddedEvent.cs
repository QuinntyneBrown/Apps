// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyTreeBuilder.Core;

public record PhotoAddedEvent
{
    public Guid FamilyPhotoId { get; init; }
    public Guid PersonId { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
