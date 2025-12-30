using MensGroupDiscussionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MensGroupDiscussionTracker.Api.Features.Resources;

public record GetResourcesQuery : IRequest<IEnumerable<ResourceDto>>
{
    public Guid? GroupId { get; init; }
    public Guid? SharedByUserId { get; init; }
    public string? ResourceType { get; init; }
}

public class GetResourcesQueryHandler : IRequestHandler<GetResourcesQuery, IEnumerable<ResourceDto>>
{
    private readonly IMensGroupDiscussionTrackerContext _context;
    private readonly ILogger<GetResourcesQueryHandler> _logger;

    public GetResourcesQueryHandler(
        IMensGroupDiscussionTrackerContext context,
        ILogger<GetResourcesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ResourceDto>> Handle(GetResourcesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting resources for group {GroupId}", request.GroupId);

        var query = _context.Resources.AsNoTracking();

        if (request.GroupId.HasValue)
        {
            query = query.Where(r => r.GroupId == request.GroupId.Value);
        }

        if (request.SharedByUserId.HasValue)
        {
            query = query.Where(r => r.SharedByUserId == request.SharedByUserId.Value);
        }

        if (!string.IsNullOrEmpty(request.ResourceType))
        {
            query = query.Where(r => r.ResourceType == request.ResourceType);
        }

        var resources = await query
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);

        return resources.Select(r => r.ToDto());
    }
}
