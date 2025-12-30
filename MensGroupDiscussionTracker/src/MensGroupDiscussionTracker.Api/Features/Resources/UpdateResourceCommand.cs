using MensGroupDiscussionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MensGroupDiscussionTracker.Api.Features.Resources;

public record UpdateResourceCommand : IRequest<ResourceDto?>
{
    public Guid ResourceId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string? Url { get; init; }
    public string? ResourceType { get; init; }
}

public class UpdateResourceCommandHandler : IRequestHandler<UpdateResourceCommand, ResourceDto?>
{
    private readonly IMensGroupDiscussionTrackerContext _context;
    private readonly ILogger<UpdateResourceCommandHandler> _logger;

    public UpdateResourceCommandHandler(
        IMensGroupDiscussionTrackerContext context,
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
        resource.Description = request.Description;
        resource.Url = request.Url;
        resource.ResourceType = request.ResourceType;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated resource {ResourceId}", request.ResourceId);

        return resource.ToDto();
    }
}
