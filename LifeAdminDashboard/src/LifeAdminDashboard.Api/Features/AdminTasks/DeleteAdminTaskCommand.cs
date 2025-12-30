using LifeAdminDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LifeAdminDashboard.Api.Features.AdminTasks;

public record DeleteAdminTaskCommand : IRequest<bool>
{
    public Guid AdminTaskId { get; init; }
}

public class DeleteAdminTaskCommandHandler : IRequestHandler<DeleteAdminTaskCommand, bool>
{
    private readonly ILifeAdminDashboardContext _context;
    private readonly ILogger<DeleteAdminTaskCommandHandler> _logger;

    public DeleteAdminTaskCommandHandler(
        ILifeAdminDashboardContext context,
        ILogger<DeleteAdminTaskCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteAdminTaskCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting admin task {AdminTaskId}", request.AdminTaskId);

        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.AdminTaskId == request.AdminTaskId, cancellationToken);

        if (task == null)
        {
            _logger.LogWarning("Admin task {AdminTaskId} not found", request.AdminTaskId);
            return false;
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted admin task {AdminTaskId}", request.AdminTaskId);

        return true;
    }
}
