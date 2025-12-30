using LifeAdminDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LifeAdminDashboard.Api.Features.Deadlines;

public record GetDeadlinesQuery : IRequest<IEnumerable<DeadlineDto>>
{
    public Guid? UserId { get; init; }
    public string? Category { get; init; }
    public bool? IsCompleted { get; init; }
    public bool? IsOverdue { get; init; }
    public bool? RequiresReminder { get; init; }
}

public class GetDeadlinesQueryHandler : IRequestHandler<GetDeadlinesQuery, IEnumerable<DeadlineDto>>
{
    private readonly ILifeAdminDashboardContext _context;
    private readonly ILogger<GetDeadlinesQueryHandler> _logger;

    public GetDeadlinesQueryHandler(
        ILifeAdminDashboardContext context,
        ILogger<GetDeadlinesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<DeadlineDto>> Handle(GetDeadlinesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting deadlines for user {UserId}", request.UserId);

        var query = _context.Deadlines.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(d => d.UserId == request.UserId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Category))
        {
            query = query.Where(d => d.Category == request.Category);
        }

        if (request.IsCompleted.HasValue)
        {
            query = query.Where(d => d.IsCompleted == request.IsCompleted.Value);
        }

        var deadlines = await query
            .OrderBy(d => d.DeadlineDateTime)
            .ToListAsync(cancellationToken);

        if (request.IsOverdue == true)
        {
            deadlines = deadlines.Where(d => d.IsOverdue()).ToList();
        }

        if (request.RequiresReminder == true)
        {
            deadlines = deadlines.Where(d => d.ShouldRemind()).ToList();
        }

        return deadlines.Select(d => d.ToDto());
    }
}
