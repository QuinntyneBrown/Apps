using MensGroupDiscussionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MensGroupDiscussionTracker.Api.Features.Meetings;

public record GetMeetingByIdQuery : IRequest<MeetingDto?>
{
    public Guid MeetingId { get; init; }
}

public class GetMeetingByIdQueryHandler : IRequestHandler<GetMeetingByIdQuery, MeetingDto?>
{
    private readonly IMensGroupDiscussionTrackerContext _context;
    private readonly ILogger<GetMeetingByIdQueryHandler> _logger;

    public GetMeetingByIdQueryHandler(
        IMensGroupDiscussionTrackerContext context,
        ILogger<GetMeetingByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MeetingDto?> Handle(GetMeetingByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting meeting {MeetingId}", request.MeetingId);

        var meeting = await _context.Meetings
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MeetingId == request.MeetingId, cancellationToken);

        return meeting?.ToDto();
    }
}
