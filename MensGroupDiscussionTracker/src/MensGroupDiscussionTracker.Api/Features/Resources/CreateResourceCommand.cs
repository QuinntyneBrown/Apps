using MensGroupDiscussionTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MensGroupDiscussionTracker.Api.Features.Resources;

public record CreateResourceCommand : IRequest<ResourceDto>
{
    public Guid GroupId { get; init; }
    public Guid SharedByUserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string? Url { get; init; }
    public string? ResourceType { get; init; }
}

public class CreateResourceCommandHandler : IRequestHandler<CreateResourceCommand, ResourceDto>
{
    private readonly IMensGroupDiscussionTrackerContext _context;
    private readonly ILogger<CreateResourceCommandHandler> _logger;

    public CreateResourceCommandHandler(
        IMensGroupDiscussionTrackerContext context,
        ILogger<CreateResourceCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ResourceDto> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating resource for group {GroupId}, title: {Title}",
            request.GroupId,
            request.Title);

        var resource = new Resource
        {
            ResourceId = Guid.NewGuid(),
            GroupId = request.GroupId,
            SharedByUserId = request.SharedByUserId,
            Title = request.Title,
            Description = request.Description,
            Url = request.Url,
            ResourceType = request.ResourceType,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Resources.Add(resource);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created resource {ResourceId} for group {GroupId}",
            resource.ResourceId,
            request.GroupId);

        return resource.ToDto();
    }
}
