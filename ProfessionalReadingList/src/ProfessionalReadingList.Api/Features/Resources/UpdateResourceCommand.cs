using ProfessionalReadingList.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalReadingList.Api.Features.Resources;

public record UpdateResourceCommand : IRequest<ResourceDto?>
{
    public Guid ResourceId { get; init; }
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

public class UpdateResourceCommandHandler : IRequestHandler<UpdateResourceCommand, ResourceDto?>
{
    private readonly IProfessionalReadingListContext _context;
    private readonly ILogger<UpdateResourceCommandHandler> _logger;

    public UpdateResourceCommandHandler(
        IProfessionalReadingListContext context,
        ILogger<UpdateResourceCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ResourceDto?> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating resource {ResourceId}", request.ResourceId);

        var resource = await _context.Resources
            .FirstOrDefaultAsync(r => r.ResourceId == request.ResourceId, cancellationToken);

        if (resource == null)
        {
            _logger.LogWarning("Resource {ResourceId} not found", request.ResourceId);
            return null;
        }

        resource.Title = request.Title;
        resource.ResourceType = request.ResourceType;
        resource.Author = request.Author;
        resource.Publisher = request.Publisher;
        resource.PublicationDate = request.PublicationDate;
        resource.Url = request.Url;
        resource.Isbn = request.Isbn;
        resource.TotalPages = request.TotalPages;
        resource.Topics = request.Topics;
        resource.Notes = request.Notes;
        resource.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated resource {ResourceId}", request.ResourceId);

        return resource.ToDto();
    }
}
