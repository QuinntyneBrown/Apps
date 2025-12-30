using PersonalMissionStatementBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalMissionStatementBuilder.Api.Features.MissionStatements;

public record GetMissionStatementByIdQuery : IRequest<MissionStatementDto?>
{
    public Guid MissionStatementId { get; init; }
}

public class GetMissionStatementByIdQueryHandler : IRequestHandler<GetMissionStatementByIdQuery, MissionStatementDto?>
{
    private readonly IPersonalMissionStatementBuilderContext _context;
    private readonly ILogger<GetMissionStatementByIdQueryHandler> _logger;

    public GetMissionStatementByIdQueryHandler(
        IPersonalMissionStatementBuilderContext context,
        ILogger<GetMissionStatementByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MissionStatementDto?> Handle(GetMissionStatementByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting mission statement {MissionStatementId}",
            request.MissionStatementId);

        var missionStatement = await _context.MissionStatements
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MissionStatementId == request.MissionStatementId, cancellationToken);

        return missionStatement?.ToDto();
    }
}
