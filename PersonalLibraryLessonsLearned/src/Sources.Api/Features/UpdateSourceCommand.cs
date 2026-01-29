using Sources.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Sources.Api.Features;

public record UpdateSourceCommand(
    Guid SourceId,
    string Title,
    string SourceType,
    string? Author,
    string? Url,
    string? Notes) : IRequest<SourceDto?>;

public class UpdateSourceCommandHandler : IRequestHandler<UpdateSourceCommand, SourceDto?>
{
    private readonly ISourcesDbContext _context;
    private readonly ILogger<UpdateSourceCommandHandler> _logger;

    public UpdateSourceCommandHandler(ISourcesDbContext context, ILogger<UpdateSourceCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SourceDto?> Handle(UpdateSourceCommand request, CancellationToken cancellationToken)
    {
        var source = await _context.Sources
            .FirstOrDefaultAsync(s => s.SourceId == request.SourceId, cancellationToken);

        if (source == null) return null;

        source.Title = request.Title;
        source.SourceType = request.SourceType;
        source.Author = request.Author;
        source.Url = request.Url;
        source.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Source updated: {SourceId}", source.SourceId);

        return source.ToDto();
    }
}
