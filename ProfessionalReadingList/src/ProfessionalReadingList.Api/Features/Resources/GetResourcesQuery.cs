using ProfessionalReadingList.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalReadingList.Api.Features.Resources;

public record GetResourcesQuery : IRequest<IEnumerable<ResourceDto>>
{
    public Guid? UserId { get; init; }
    public ResourceType? ResourceType { get; init; }
    public string? Topic { get; init; }
}

public class GetResourcesQueryHandler : IRequestHandler<GetResourcesQuery, IEnumerable<ResourceDto>>
{
    private readonly IProfessionalReadingListContext _context;
    private readonly ILogger<GetResourcesQueryHandler> _logger;

    public GetResourcesQueryHandler(
        IProfessionalReadingListContext context,
        ILogger<GetResourcesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ResourceDto>> Handle(GetResourcesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting resources for user {UserId}", request.UserId);

        var query = _context.Resources.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(r => r.UserId == request.UserId.Value);
        }

        if (request.ResourceType.HasValue)
        {
            query = query.Where(r => r.ResourceType == request.ResourceType.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Topic))
        {
            query = query.Where(r => r.Topics.Contains(request.Topic));
        }

        var resources = await query
            .OrderByDescending(r => r.DateAdded)
            .ToListAsync(cancellationToken);

        return resources.Select(r => r.ToDto());
    }
}
