using PersonalMissionStatementBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalMissionStatementBuilder.Api.Features.MissionStatements;

public record GetMissionStatementsQuery : IRequest<IEnumerable<MissionStatementDto>>
{
    public Guid? UserId { get; init; }
    public bool? CurrentVersionOnly { get; init; }
}

public class GetMissionStatementsQueryHandler : IRequestHandler<GetMissionStatementsQuery, IEnumerable<MissionStatementDto>>
{
    private readonly IPersonalMissionStatementBuilderContext _context;
    private readonly ILogger<GetMissionStatementsQueryHandler> _logger;

    public GetMissionStatementsQueryHandler(
        IPersonalMissionStatementBuilderContext context,
        ILogger<GetMissionStatementsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<MissionStatementDto>> Handle(GetMissionStatementsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting mission statements for user {UserId}", request.UserId);

        var query = _context.MissionStatements.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(m => m.UserId == request.UserId.Value);
        }

        if (request.CurrentVersionOnly == true)
        {
            query = query.Where(m => m.IsCurrentVersion);
        }

        var missionStatements = await query
            .OrderByDescending(m => m.StatementDate)
            .ToListAsync(cancellationToken);

        return missionStatements.Select(m => m.ToDto());
    }
}
