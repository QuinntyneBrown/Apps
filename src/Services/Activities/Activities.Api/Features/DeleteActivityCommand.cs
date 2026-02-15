using Activities.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Activities.Api.Features;

public record DeleteActivityCommand(Guid ActivityId) : IRequest<bool>;

public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand, bool>
{
    private readonly IActivitiesDbContext _context;
    private readonly ILogger<DeleteActivityCommandHandler> _logger;

    public DeleteActivityCommandHandler(IActivitiesDbContext context, ILogger<DeleteActivityCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = await _context.Activities
            .FirstOrDefaultAsync(a => a.ActivityId == request.ActivityId, cancellationToken);

        if (activity == null) return false;

        _context.Activities.Remove(activity);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Activity deleted: {ActivityId}", request.ActivityId);

        return true;
    }
}
