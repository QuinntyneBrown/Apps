using Sources.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Sources.Api.Features;

public record DeleteSourceCommand(Guid SourceId) : IRequest<bool>;

public class DeleteSourceCommandHandler : IRequestHandler<DeleteSourceCommand, bool>
{
    private readonly ISourcesDbContext _context;
    private readonly ILogger<DeleteSourceCommandHandler> _logger;

    public DeleteSourceCommandHandler(ISourcesDbContext context, ILogger<DeleteSourceCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteSourceCommand request, CancellationToken cancellationToken)
    {
        var source = await _context.Sources
            .FirstOrDefaultAsync(s => s.SourceId == request.SourceId, cancellationToken);

        if (source == null) return false;

        _context.Sources.Remove(source);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Source deleted: {SourceId}", request.SourceId);

        return true;
    }
}
