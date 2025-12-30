// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyTreeBuilder.Core;

public record StoryAddedEvent
{
    public Guid StoryId { get; init; }
    public Guid PersonId { get; init; }
    public string Title { get; init; } = string.Empty;
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
