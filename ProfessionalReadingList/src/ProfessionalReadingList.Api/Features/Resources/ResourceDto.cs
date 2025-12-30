using ProfessionalReadingList.Core;

namespace ProfessionalReadingList.Api.Features.Resources;

public record ResourceDto
{
    public Guid ResourceId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public ResourceType ResourceType { get; init; }
    public string? Author { get; init; }
    public string? Publisher { get; init; }
    public DateTime? PublicationDate { get; init; }
    public string? Url { get; init; }
    public string? Isbn { get; init; }
    public int? TotalPages { get; init; }
    public List<string> Topics { get; init; } = new List<string>();
    public DateTime DateAdded { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class ResourceExtensions
{
    public static ResourceDto ToDto(this Resource resource)
    {
        return new ResourceDto
        {
            ResourceId = resource.ResourceId,
            UserId = resource.UserId,
            Title = resource.Title,
            ResourceType = resource.ResourceType,
            Author = resource.Author,
            Publisher = resource.Publisher,
            PublicationDate = resource.PublicationDate,
            Url = resource.Url,
            Isbn = resource.Isbn,
            TotalPages = resource.TotalPages,
            Topics = resource.Topics,
            DateAdded = resource.DateAdded,
            Notes = resource.Notes,
            CreatedAt = resource.CreatedAt,
            UpdatedAt = resource.UpdatedAt,
        };
    }
}
