using PersonalMissionStatementBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalMissionStatementBuilder.Api.Features.Progresses;

public record UpdateProgressCommand : IRequest<ProgressDto>
{
    public Guid ProgressId { get; init; }
    public DateTime? ProgressDate { get; init; }
    public string? Notes { get; init; }
    public double? CompletionPercentage { get; init; }
}

public class UpdateProgressCommandHandler : IRequestHandler<UpdateProgressCommand, ProgressDto>
{
    private readonly IPersonalMissionStatementBuilderContext _context;
    private readonly ILogger<UpdateProgressCommandHandler> _logger;

    public UpdateProgressCommandHandler(
        IPersonalMissionStatementBuilderContext context,
        ILogger<UpdateProgressCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ProgressDto> Handle(UpdateProgressCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Updating progress {ProgressId}",
            request.ProgressId);

        var progress = await _context.Progresses
            .FirstOrDefaultAsync(p => p.ProgressId == request.ProgressId, cancellationToken);

        if (progress == null)
        {
            throw new InvalidOperationException($"Progress with ID {request.ProgressId} not found");
        }

        if (request.ProgressDate.HasValue)
        {
            progress.ProgressDate = request.ProgressDate.Value;
            progress.UpdatedAt = DateTime.UtcNow;
        }

        if (!string.IsNullOrEmpty(request.Notes))
        {
            progress.Notes = request.Notes;
            progress.UpdatedAt = DateTime.UtcNow;
        }

        if (request.CompletionPercentage.HasValue)
        {
            progress.UpdatePercentage(request.CompletionPercentage.Value);
        }

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Updated progress {ProgressId}",
            request.ProgressId);

        return progress.ToDto();
    }
}
