using MensGroupDiscussionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MensGroupDiscussionTracker.Api.Features.Meetings;

public record GetMeetingsQuery : IRequest<IEnumerable<MeetingDto>>
{
    public Guid? GroupId { get; init; }
}

public class GetMeetingsQueryHandler : IRequestHandler<GetMeetingsQuery, IEnumerable<MeetingDto>>
{
    private readonly IMensGroupDiscussionTrackerContext _context;
    private readonly ILogger<GetMeetingsQueryHandler> _logger;

    public GetMeetingsQueryHandler(
        IMensGroupDiscussionTrackerContext context,
        ILogger<GetMeetingsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<MeetingDto>> Handle(GetMeetingsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting meetings for group {GroupId}", request.GroupId);

        var query = _context.Meetings.AsNoTracking();

        if (request.GroupId.HasValue)
        {
            query = query.Where(m => m.GroupId == request.GroupId.Value);
        }

        var meetings = await query
            .OrderByDescending(m => m.MeetingDateTime)
            .ToListAsync(cancellationToken);

        return meetings.Select(m => m.ToDto());
    }
}
