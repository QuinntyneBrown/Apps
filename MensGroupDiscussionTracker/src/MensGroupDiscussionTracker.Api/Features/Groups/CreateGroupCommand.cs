using MensGroupDiscussionTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MensGroupDiscussionTracker.Api.Features.Groups;

public record CreateGroupCommand : IRequest<GroupDto>
{
    public Guid CreatedByUserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string? MeetingSchedule { get; init; }
}

public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, GroupDto>
{
    private readonly IMensGroupDiscussionTrackerContext _context;
    private readonly ILogger<CreateGroupCommandHandler> _logger;

    public CreateGroupCommandHandler(
        IMensGroupDiscussionTrackerContext context,
        ILogger<CreateGroupCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GroupDto> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating group for user {UserId}, name: {Name}",
            request.CreatedByUserId,
            request.Name);

        var group = new Group
        {
            GroupId = Guid.NewGuid(),
            CreatedByUserId = request.CreatedByUserId,
            Name = request.Name,
            Description = request.Description,
            MeetingSchedule = request.MeetingSchedule,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Groups.Add(group);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created group {GroupId} for user {UserId}",
            group.GroupId,
            request.CreatedByUserId);

        return group.ToDto();
    }
}
