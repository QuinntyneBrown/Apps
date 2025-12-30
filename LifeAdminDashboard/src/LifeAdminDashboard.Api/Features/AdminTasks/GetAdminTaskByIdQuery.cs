using LifeAdminDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LifeAdminDashboard.Api.Features.AdminTasks;

public record GetAdminTaskByIdQuery : IRequest<AdminTaskDto?>
{
    public Guid AdminTaskId { get; init; }
}

public class GetAdminTaskByIdQueryHandler : IRequestHandler<GetAdminTaskByIdQuery, AdminTaskDto?>
{
    private readonly ILifeAdminDashboardContext _context;
    private readonly ILogger<GetAdminTaskByIdQueryHandler> _logger;

    public GetAdminTaskByIdQueryHandler(
        ILifeAdminDashboardContext context,
        ILogger<GetAdminTaskByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<AdminTaskDto?> Handle(GetAdminTaskByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting admin task {AdminTaskId}", request.AdminTaskId);

        var task = await _context.Tasks
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.AdminTaskId == request.AdminTaskId, cancellationToken);

        return task?.ToDto();
    }
}
