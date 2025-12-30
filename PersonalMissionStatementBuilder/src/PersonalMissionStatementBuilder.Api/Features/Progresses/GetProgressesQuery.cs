using PersonalMissionStatementBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalMissionStatementBuilder.Api.Features.Progresses;

public record GetProgressesQuery : IRequest<IEnumerable<ProgressDto>>
{
    public Guid? UserId { get; init; }
    public Guid? GoalId { get; init; }
}

public class GetProgressesQueryHandler : IRequestHandler<GetProgressesQuery, IEnumerable<ProgressDto>>
{
    private readonly IPersonalMissionStatementBuilderContext _context;
    private readonly ILogger<GetProgressesQueryHandler> _logger;

    public GetProgressesQueryHandler(
        IPersonalMissionStatementBuilderContext context,
        ILogger<GetProgressesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ProgressDto>> Handle(GetProgressesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting progresses for user {UserId}", request.UserId);

        var query = _context.Progresses.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(p => p.UserId == request.UserId.Value);
        }

        if (request.GoalId.HasValue)
        {
            query = query.Where(p => p.GoalId == request.GoalId.Value);
        }

        var progresses = await query
            .OrderByDescending(p => p.ProgressDate)
            .ToListAsync(cancellationToken);

        return progresses.Select(p => p.ToDto());
    }
}
