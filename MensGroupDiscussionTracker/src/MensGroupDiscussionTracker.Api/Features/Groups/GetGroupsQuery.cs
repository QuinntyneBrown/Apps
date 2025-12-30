using MensGroupDiscussionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MensGroupDiscussionTracker.Api.Features.Groups;

public record GetGroupsQuery : IRequest<IEnumerable<GroupDto>>
{
    public Guid? CreatedByUserId { get; init; }
    public bool? IsActive { get; init; }
}

public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, IEnumerable<GroupDto>>
{
    private readonly IMensGroupDiscussionTrackerContext _context;
    private readonly ILogger<GetGroupsQueryHandler> _logger;

    public GetGroupsQueryHandler(
        IMensGroupDiscussionTrackerContext context,
        ILogger<GetGroupsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<GroupDto>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting groups for user {UserId}", request.CreatedByUserId);

        var query = _context.Groups.AsNoTracking();

        if (request.CreatedByUserId.HasValue)
        {
            query = query.Where(g => g.CreatedByUserId == request.CreatedByUserId.Value);
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(g => g.IsActive == request.IsActive.Value);
        }

        var groups = await query
            .OrderByDescending(g => g.CreatedAt)
            .ToListAsync(cancellationToken);

        return groups.Select(g => g.ToDto());
    }
}
