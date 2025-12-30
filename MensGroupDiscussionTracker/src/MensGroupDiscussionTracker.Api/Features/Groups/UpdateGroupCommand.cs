using MensGroupDiscussionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MensGroupDiscussionTracker.Api.Features.Groups;

public record UpdateGroupCommand : IRequest<GroupDto?>
{
    public Guid GroupId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string? MeetingSchedule { get; init; }
    public bool IsActive { get; init; }
}

public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, GroupDto?>
{
    private readonly IMensGroupDiscussionTrackerContext _context;
    private readonly ILogger<UpdateGroupCommandHandler> _logger;

    public UpdateGroupCommandHandler(
        IMensGroupDiscussionTrackerContext context,
        ILogger<UpdateGroupCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GroupDto?> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating group {GroupId}", request.GroupId);

        var group = await _context.Groups
            .FirstOrDefaultAsync(g => g.GroupId == request.GroupId, cancellationToken);

        if (group == null)
        {
            _logger.LogWarning("Group {GroupId} not found", request.GroupId);
            return null;
        }

        group.Name = request.Name;
        group.Description = request.Description;
        group.MeetingSchedule = request.MeetingSchedule;
        group.IsActive = request.IsActive;
        group.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated group {GroupId}", request.GroupId);

        return group.ToDto();
    }
}
