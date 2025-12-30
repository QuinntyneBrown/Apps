using MensGroupDiscussionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MensGroupDiscussionTracker.Api.Features.Meetings;

public record DeleteMeetingCommand : IRequest<bool>
{
    public Guid MeetingId { get; init; }
}

public class DeleteMeetingCommandHandler : IRequestHandler<DeleteMeetingCommand, bool>
{
    private readonly IMensGroupDiscussionTrackerContext _context;
    private readonly ILogger<DeleteMeetingCommandHandler> _logger;

    public DeleteMeetingCommandHandler(
        IMensGroupDiscussionTrackerContext context,
        ILogger<DeleteMeetingCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteMeetingCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting meeting {MeetingId}", request.MeetingId);

        var meeting = await _context.Meetings
            .FirstOrDefaultAsync(m => m.MeetingId == request.MeetingId, cancellationToken);

        if (meeting == null)
        {
            _logger.LogWarning("Meeting {MeetingId} not found", request.MeetingId);
            return false;
        }

        _context.Meetings.Remove(meeting);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted meeting {MeetingId}", request.MeetingId);

        return true;
    }
}
