using PersonalMissionStatementBuilder.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PersonalMissionStatementBuilder.Api.Features.MissionStatements;

public record CreateMissionStatementCommand : IRequest<MissionStatementDto>
{
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Text { get; init; } = string.Empty;
    public DateTime? StatementDate { get; init; }
}

public class CreateMissionStatementCommandHandler : IRequestHandler<CreateMissionStatementCommand, MissionStatementDto>
{
    private readonly IPersonalMissionStatementBuilderContext _context;
    private readonly ILogger<CreateMissionStatementCommandHandler> _logger;

    public CreateMissionStatementCommandHandler(
        IPersonalMissionStatementBuilderContext context,
        ILogger<CreateMissionStatementCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MissionStatementDto> Handle(CreateMissionStatementCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating mission statement for user {UserId}, title: {Title}",
            request.UserId,
            request.Title);

        var missionStatement = new MissionStatement
        {
            MissionStatementId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Text = request.Text,
            Version = 1,
            IsCurrentVersion = true,
            StatementDate = request.StatementDate ?? DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        _context.MissionStatements.Add(missionStatement);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created mission statement {MissionStatementId} for user {UserId}",
            missionStatement.MissionStatementId,
            request.UserId);

        return missionStatement.ToDto();
    }
}
