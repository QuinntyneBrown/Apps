using LifeAdminDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LifeAdminDashboard.Api.Features.Deadlines;

public record GetDeadlineByIdQuery : IRequest<DeadlineDto?>
{
    public Guid DeadlineId { get; init; }
}

public class GetDeadlineByIdQueryHandler : IRequestHandler<GetDeadlineByIdQuery, DeadlineDto?>
{
    private readonly ILifeAdminDashboardContext _context;
    private readonly ILogger<GetDeadlineByIdQueryHandler> _logger;

    public GetDeadlineByIdQueryHandler(
        ILifeAdminDashboardContext context,
        ILogger<GetDeadlineByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<DeadlineDto?> Handle(GetDeadlineByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting deadline {DeadlineId}", request.DeadlineId);

        var deadline = await _context.Deadlines
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.DeadlineId == request.DeadlineId, cancellationToken);

        return deadline?.ToDto();
    }
}
