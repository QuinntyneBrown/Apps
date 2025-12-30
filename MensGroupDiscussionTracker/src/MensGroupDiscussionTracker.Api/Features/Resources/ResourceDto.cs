using MensGroupDiscussionTracker.Core;

namespace MensGroupDiscussionTracker.Api.Features.Resources;

public record ResourceDto
{
    public Guid ResourceId { get; init; }
    public Guid GroupId { get; init; }
    public Guid SharedByUserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string? Url { get; init; }
    public string? ResourceType { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class ResourceExtensions
{
    public static ResourceDto ToDto(this Resource resource)
    {
        return new ResourceDto
        {
            ResourceId = resource.ResourceId,
            GroupId = resource.GroupId,
            SharedByUserId = resource.SharedByUserId,
            Title = resource.Title,
            Description = resource.Description,
            Url = resource.Url,
            ResourceType = resource.ResourceType,
            CreatedAt = resource.CreatedAt,
        };
    }
}
