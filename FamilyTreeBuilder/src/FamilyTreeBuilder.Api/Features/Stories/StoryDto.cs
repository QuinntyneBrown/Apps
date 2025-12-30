// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;

namespace FamilyTreeBuilder.Api.Features.Stories;

public class StoryDto
{
    public Guid StoryId { get; set; }
    public Guid PersonId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; }

    public static StoryDto FromStory(Story story)
    {
        return new StoryDto
        {
            StoryId = story.StoryId,
            PersonId = story.PersonId,
            Title = story.Title,
            Content = story.Content,
            CreatedAt = story.CreatedAt
        };
    }
}
