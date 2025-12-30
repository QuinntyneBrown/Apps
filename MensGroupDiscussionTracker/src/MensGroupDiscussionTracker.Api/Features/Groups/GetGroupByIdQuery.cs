using MensGroupDiscussionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MensGroupDiscussionTracker.Api.Features.Groups;

public record GetGroupByIdQuery : IRequest<GroupDto?>
{
    public Guid GroupId { get; init; }
}

public class GetGroupByIdQueryHandler : IRequestHandler<GetGroupByIdQuery, GroupDto?>
{
    private readonly IMensGroupDiscussionTrackerContext _context;
    private readonly ILogger<GetGroupByIdQueryHandler> _logger;

    public GetGroupByIdQueryHandler(
        IMensGroupDiscussionTrackerContext context,
        ILogger<GetGroupByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GroupDto?> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting group {GroupId}", request.GroupId);

        var group = await _context.Groups
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.GroupId == request.GroupId, cancellationToken);

        return group?.ToDto();
    }
}
