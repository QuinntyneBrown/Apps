using PersonalMissionStatementBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalMissionStatementBuilder.Api.Features.MissionStatements;

public record DeleteMissionStatementCommand : IRequest<bool>
{
    public Guid MissionStatementId { get; init; }
}

public class DeleteMissionStatementCommandHandler : IRequestHandler<DeleteMissionStatementCommand, bool>
{
    private readonly IPersonalMissionStatementBuilderContext _context;
    private readonly ILogger<DeleteMissionStatementCommandHandler> _logger;

    public DeleteMissionStatementCommandHandler(
        IPersonalMissionStatementBuilderContext context,
        ILogger<DeleteMissionStatementCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteMissionStatementCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Deleting mission statement {MissionStatementId}",
            request.MissionStatementId);

        var missionStatement = await _context.MissionStatements
            .FirstOrDefaultAsync(m => m.MissionStatementId == request.MissionStatementId, cancellationToken);

        if (missionStatement == null)
        {
            _logger.LogWarning(
                "Mission statement {MissionStatementId} not found for deletion",
                request.MissionStatementId);
            return false;
        }

        _context.MissionStatements.Remove(missionStatement);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Deleted mission statement {MissionStatementId}",
            request.MissionStatementId);

        return true;
    }
}
