using MensGroupDiscussionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MensGroupDiscussionTracker.Api.Features.Meetings;

public record UpdateMeetingCommand : IRequest<MeetingDto?>
{
    public Guid MeetingId { get; init; }
    public string Title { get; init; } = string.Empty;
    public DateTime MeetingDateTime { get; init; }
    public string? Location { get; init; }
    public string? Notes { get; init; }
    public int AttendeeCount { get; init; }
}

public class UpdateMeetingCommandHandler : IRequestHandler<UpdateMeetingCommand, MeetingDto?>
{
    private readonly IMensGroupDiscussionTrackerContext _context;
    private readonly ILogger<UpdateMeetingCommandHandler> _logger;

    public UpdateMeetingCommandHandler(
        IMensGroupDiscussionTrackerContext context,
        ILogger<UpdateMeetingCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MeetingDto?> Handle(UpdateMeetingCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating meeting {MeetingId}", request.MeetingId);

        var meeting = await _context.Meetings
            .FirstOrDefaultAsync(m => m.MeetingId == request.MeetingId, cancellationToken);

        if (meeting == null)
        {
            _logger.LogWarning("Meeting {MeetingId} not found", request.MeetingId);
            return null;
        }

        meeting.Title = request.Title;
        meeting.MeetingDateTime = request.MeetingDateTime;
        meeting.Location = request.Location;
        meeting.Notes = request.Notes;
        meeting.AttendeeCount = request.AttendeeCount;
        meeting.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated meeting {MeetingId}", request.MeetingId);

        return meeting.ToDto();
    }
}
