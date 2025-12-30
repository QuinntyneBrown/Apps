using MensGroupDiscussionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MensGroupDiscussionTracker.Api.Features.Resources;

public record GetResourceByIdQuery : IRequest<ResourceDto?>
{
    public Guid ResourceId { get; init; }
}

public class GetResourceByIdQueryHandler : IRequestHandler<GetResourceByIdQuery, ResourceDto?>
{
    private readonly IMensGroupDiscussionTrackerContext _context;
    private readonly ILogger<GetResourceByIdQueryHandler> _logger;

    public GetResourceByIdQueryHandler(
        IMensGroupDiscussionTrackerContext context,
        ILogger<GetResourceByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ResourceDto?> Handle(GetResourceByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting resource {ResourceId}", request.ResourceId);

        var resource = await _context.Resources
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.ResourceId == request.ResourceId, cancellationToken);

        return resource?.ToDto();
    }
}
