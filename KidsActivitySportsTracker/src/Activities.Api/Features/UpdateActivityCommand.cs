using Activities.Core;
using Activities.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Activities.Api.Features;

public record UpdateActivityCommand(
    Guid ActivityId,
    string Name,
    ActivityType Type,
    string? Description,
    string? Location,
    string? CoachName,
    string? ContactInfo,
    decimal? Cost,
    bool IsActive) : IRequest<ActivityDto?>;

public class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, ActivityDto?>
{
    private readonly IActivitiesDbContext _context;
    private readonly ILogger<UpdateActivityCommandHandler> _logger;

    public UpdateActivityCommandHandler(IActivitiesDbContext context, ILogger<UpdateActivityCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ActivityDto?> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = await _context.Activities
            .FirstOrDefaultAsync(a => a.ActivityId == request.ActivityId, cancellationToken);

        if (activity == null) return null;

        activity.Name = request.Name;
        activity.Type = request.Type;
        activity.Description = request.Description;
        activity.Location = request.Location;
        activity.CoachName = request.CoachName;
        activity.ContactInfo = request.ContactInfo;
        activity.Cost = request.Cost;
        activity.IsActive = request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Activity updated: {ActivityId}", activity.ActivityId);

        return activity.ToDto();
    }
}
