using LifeAdminDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LifeAdminDashboard.Api.Features.Deadlines;

public record DeleteDeadlineCommand : IRequest<bool>
{
    public Guid DeadlineId { get; init; }
}

public class DeleteDeadlineCommandHandler : IRequestHandler<DeleteDeadlineCommand, bool>
{
    private readonly ILifeAdminDashboardContext _context;
    private readonly ILogger<DeleteDeadlineCommandHandler> _logger;

    public DeleteDeadlineCommandHandler(
        ILifeAdminDashboardContext context,
        ILogger<DeleteDeadlineCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteDeadlineCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting deadline {DeadlineId}", request.DeadlineId);

        var deadline = await _context.Deadlines
            .FirstOrDefaultAsync(d => d.DeadlineId == request.DeadlineId, cancellationToken);

        if (deadline == null)
        {
            _logger.LogWarning("Deadline {DeadlineId} not found", request.DeadlineId);
            return false;
        }

        _context.Deadlines.Remove(deadline);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted deadline {DeadlineId}", request.DeadlineId);

        return true;
    }
}
