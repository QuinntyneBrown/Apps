using PerformanceReviewPrepTool.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PerformanceReviewPrepTool.Api.Features.Goals;

public record GetGoalByIdQuery : IRequest<GoalDto?>
{
    public Guid GoalId { get; init; }
}

public class GetGoalByIdQueryHandler : IRequestHandler<GetGoalByIdQuery, GoalDto?>
{
    private readonly IPerformanceReviewPrepToolContext _context;
    private readonly ILogger<GetGoalByIdQueryHandler> _logger;

    public GetGoalByIdQueryHandler(
        IPerformanceReviewPrepToolContext context,
        ILogger<GetGoalByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GoalDto?> Handle(GetGoalByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting goal {GoalId}", request.GoalId);

        var goal = await _context.Goals
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.GoalId == request.GoalId, cancellationToken);

        if (goal == null)
        {
            _logger.LogWarning("Goal {GoalId} not found", request.GoalId);
            return null;
        }

        return goal.ToDto();
    }
}
