using ProfessionalReadingList.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ProfessionalReadingList.Api.Features.Resources;

public record CreateResourceCommand : IRequest<ResourceDto>
{
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
    public string? Notes { get; init; }
}

public class CreateResourceCommandHandler : IRequestHandler<CreateResourceCommand, ResourceDto>
{
    private readonly IProfessionalReadingListContext _context;
    private readonly ILogger<CreateResourceCommandHandler> _logger;

    public CreateResourceCommandHandler(
        IProfessionalReadingListContext context,
        ILogger<CreateResourceCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ResourceDto> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating resource for user {UserId}, title: {Title}",
            request.UserId,
            request.Title);

        var resource = new Resource
        {
            ResourceId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            ResourceType = request.ResourceType,
            Author = request.Author,
            Publisher = request.Publisher,
            PublicationDate = request.PublicationDate,
            Url = request.Url,
            Isbn = request.Isbn,
            TotalPages = request.TotalPages,
            Topics = request.Topics,
            Notes = request.Notes,
            DateAdded = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Resources.Add(resource);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created resource {ResourceId} for user {UserId}",
            resource.ResourceId,
            request.UserId);

        return resource.ToDto();
    }
}
