using PersonalMissionStatementBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalMissionStatementBuilder.Api.Features.MissionStatements;

public record UpdateMissionStatementCommand : IRequest<MissionStatementDto>
{
    public Guid MissionStatementId { get; init; }
    public string? Title { get; init; }
    public string? Text { get; init; }
    public bool? IsCurrentVersion { get; init; }
}

public class UpdateMissionStatementCommandHandler : IRequestHandler<UpdateMissionStatementCommand, MissionStatementDto>
{
    private readonly IPersonalMissionStatementBuilderContext _context;
    private readonly ILogger<UpdateMissionStatementCommandHandler> _logger;

    public UpdateMissionStatementCommandHandler(
        IPersonalMissionStatementBuilderContext context,
        ILogger<UpdateMissionStatementCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MissionStatementDto> Handle(UpdateMissionStatementCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Updating mission statement {MissionStatementId}",
            request.MissionStatementId);

        var missionStatement = await _context.MissionStatements
            .FirstOrDefaultAsync(m => m.MissionStatementId == request.MissionStatementId, cancellationToken);

        if (missionStatement == null)
        {
            throw new InvalidOperationException($"Mission statement with ID {request.MissionStatementId} not found");
        }

        if (!string.IsNullOrEmpty(request.Title))
        {
            missionStatement.Title = request.Title;
        }

        if (!string.IsNullOrEmpty(request.Text))
        {
            missionStatement.UpdateText(request.Text);
        }

        if (request.IsCurrentVersion.HasValue)
        {
            missionStatement.IsCurrentVersion = request.IsCurrentVersion.Value;
            missionStatement.UpdatedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Updated mission statement {MissionStatementId}",
            request.MissionStatementId);

        return missionStatement.ToDto();
    }
}
