using PersonalLibraryLessonsLearned.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalLibraryLessonsLearned.Api.Features.Source;

public record UpdateSourceCommand : IRequest<SourceDto?>
{
    public Guid SourceId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Author { get; init; }
    public string SourceType { get; init; } = string.Empty;
    public string? Url { get; init; }
    public DateTime? DateConsumed { get; init; }
    public string? Notes { get; init; }
}

public class UpdateSourceCommandHandler : IRequestHandler<UpdateSourceCommand, SourceDto?>
{
    private readonly IPersonalLibraryLessonsLearnedContext _context;
    private readonly ILogger<UpdateSourceCommandHandler> _logger;

    public UpdateSourceCommandHandler(
        IPersonalLibraryLessonsLearnedContext context,
        ILogger<UpdateSourceCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SourceDto?> Handle(UpdateSourceCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating source {SourceId}", request.SourceId);

        var source = await _context.Sources
            .FirstOrDefaultAsync(s => s.SourceId == request.SourceId, cancellationToken);

        if (source == null)
        {
            _logger.LogWarning("Source {SourceId} not found", request.SourceId);
            return null;
        }

        source.Title = request.Title;
        source.Author = request.Author;
        source.SourceType = request.SourceType;
        source.Url = request.Url;
        source.DateConsumed = request.DateConsumed;
        source.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated source {SourceId}", request.SourceId);

        return source.ToDto();
    }
}
