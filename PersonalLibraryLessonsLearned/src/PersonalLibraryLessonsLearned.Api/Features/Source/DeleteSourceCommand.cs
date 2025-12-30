using PersonalLibraryLessonsLearned.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalLibraryLessonsLearned.Api.Features.Source;

public record DeleteSourceCommand : IRequest<bool>
{
    public Guid SourceId { get; init; }
}

public class DeleteSourceCommandHandler : IRequestHandler<DeleteSourceCommand, bool>
{
    private readonly IPersonalLibraryLessonsLearnedContext _context;
    private readonly ILogger<DeleteSourceCommandHandler> _logger;

    public DeleteSourceCommandHandler(
        IPersonalLibraryLessonsLearnedContext context,
        ILogger<DeleteSourceCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteSourceCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting source {SourceId}", request.SourceId);

        var source = await _context.Sources
            .FirstOrDefaultAsync(s => s.SourceId == request.SourceId, cancellationToken);

        if (source == null)
        {
            _logger.LogWarning("Source {SourceId} not found", request.SourceId);
            return false;
        }

        _context.Sources.Remove(source);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted source {SourceId}", request.SourceId);

        return true;
    }
}
