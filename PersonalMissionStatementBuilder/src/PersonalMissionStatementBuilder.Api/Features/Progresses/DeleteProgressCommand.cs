using PersonalMissionStatementBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalMissionStatementBuilder.Api.Features.Progresses;

public record DeleteProgressCommand : IRequest<bool>
{
    public Guid ProgressId { get; init; }
}

public class DeleteProgressCommandHandler : IRequestHandler<DeleteProgressCommand, bool>
{
    private readonly IPersonalMissionStatementBuilderContext _context;
    private readonly ILogger<DeleteProgressCommandHandler> _logger;

    public DeleteProgressCommandHandler(
        IPersonalMissionStatementBuilderContext context,
        ILogger<DeleteProgressCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteProgressCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Deleting progress {ProgressId}",
            request.ProgressId);

        var progress = await _context.Progresses
            .FirstOrDefaultAsync(p => p.ProgressId == request.ProgressId, cancellationToken);

        if (progress == null)
        {
            _logger.LogWarning(
                "Progress {ProgressId} not found for deletion",
                request.ProgressId);
            return false;
        }

        _context.Progresses.Remove(progress);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Deleted progress {ProgressId}",
            request.ProgressId);

        return true;
    }
}
