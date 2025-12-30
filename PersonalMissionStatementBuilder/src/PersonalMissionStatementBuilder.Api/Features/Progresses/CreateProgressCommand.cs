using PersonalMissionStatementBuilder.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PersonalMissionStatementBuilder.Api.Features.Progresses;

public record CreateProgressCommand : IRequest<ProgressDto>
{
    public Guid GoalId { get; init; }
    public Guid UserId { get; init; }
    public DateTime? ProgressDate { get; init; }
    public string Notes { get; init; } = string.Empty;
    public double CompletionPercentage { get; init; }
}

public class CreateProgressCommandHandler : IRequestHandler<CreateProgressCommand, ProgressDto>
{
    private readonly IPersonalMissionStatementBuilderContext _context;
    private readonly ILogger<CreateProgressCommandHandler> _logger;

    public CreateProgressCommandHandler(
        IPersonalMissionStatementBuilderContext context,
        ILogger<CreateProgressCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ProgressDto> Handle(CreateProgressCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating progress for goal {GoalId}, user {UserId}",
            request.GoalId,
            request.UserId);

        var progress = new Progress
        {
            ProgressId = Guid.NewGuid(),
            GoalId = request.GoalId,
            UserId = request.UserId,
            ProgressDate = request.ProgressDate ?? DateTime.UtcNow,
            Notes = request.Notes,
            CompletionPercentage = request.CompletionPercentage,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Progresses.Add(progress);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created progress {ProgressId} for goal {GoalId}",
            progress.ProgressId,
            request.GoalId);

        return progress.ToDto();
    }
}
