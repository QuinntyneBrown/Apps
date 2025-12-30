using MensGroupDiscussionTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MensGroupDiscussionTracker.Api.Features.Meetings;

public record CreateMeetingCommand : IRequest<MeetingDto>
{
    public Guid GroupId { get; init; }
    public string Title { get; init; } = string.Empty;
    public DateTime MeetingDateTime { get; init; }
    public string? Location { get; init; }
    public string? Notes { get; init; }
    public int AttendeeCount { get; init; }
}

public class CreateMeetingCommandHandler : IRequestHandler<CreateMeetingCommand, MeetingDto>
{
    private readonly IMensGroupDiscussionTrackerContext _context;
    private readonly ILogger<CreateMeetingCommandHandler> _logger;

    public CreateMeetingCommandHandler(
        IMensGroupDiscussionTrackerContext context,
        ILogger<CreateMeetingCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MeetingDto> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating meeting for group {GroupId}, title: {Title}",
            request.GroupId,
            request.Title);

        var meeting = new Meeting
        {
            MeetingId = Guid.NewGuid(),
            GroupId = request.GroupId,
            Title = request.Title,
            MeetingDateTime = request.MeetingDateTime,
            Location = request.Location,
            Notes = request.Notes,
            AttendeeCount = request.AttendeeCount,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Meetings.Add(meeting);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created meeting {MeetingId} for group {GroupId}",
            meeting.MeetingId,
            request.GroupId);

        return meeting.ToDto();
    }
}
