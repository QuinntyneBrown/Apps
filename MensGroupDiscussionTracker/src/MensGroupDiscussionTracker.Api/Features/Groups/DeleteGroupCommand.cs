using MensGroupDiscussionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MensGroupDiscussionTracker.Api.Features.Groups;

public record DeleteGroupCommand : IRequest<bool>
{
    public Guid GroupId { get; init; }
}

public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, bool>
{
    private readonly IMensGroupDiscussionTrackerContext _context;
    private readonly ILogger<DeleteGroupCommandHandler> _logger;

    public DeleteGroupCommandHandler(
        IMensGroupDiscussionTrackerContext context,
        ILogger<DeleteGroupCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting group {GroupId}", request.GroupId);

        var group = await _context.Groups
            .FirstOrDefaultAsync(g => g.GroupId == request.GroupId, cancellationToken);

        if (group == null)
        {
            _logger.LogWarning("Group {GroupId} not found", request.GroupId);
            return false;
        }

        _context.Groups.Remove(group);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted group {GroupId}", request.GroupId);

        return true;
    }
}
