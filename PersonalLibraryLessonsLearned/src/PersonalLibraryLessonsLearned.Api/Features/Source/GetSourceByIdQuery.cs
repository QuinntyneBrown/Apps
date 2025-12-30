using PersonalLibraryLessonsLearned.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalLibraryLessonsLearned.Api.Features.Source;

public record GetSourceByIdQuery : IRequest<SourceDto?>
{
    public Guid SourceId { get; init; }
}

public class GetSourceByIdQueryHandler : IRequestHandler<GetSourceByIdQuery, SourceDto?>
{
    private readonly IPersonalLibraryLessonsLearnedContext _context;
    private readonly ILogger<GetSourceByIdQueryHandler> _logger;

    public GetSourceByIdQueryHandler(
        IPersonalLibraryLessonsLearnedContext context,
        ILogger<GetSourceByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SourceDto?> Handle(GetSourceByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting source {SourceId}", request.SourceId);

        var source = await _context.Sources
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.SourceId == request.SourceId, cancellationToken);

        if (source == null)
        {
            _logger.LogWarning("Source {SourceId} not found", request.SourceId);
            return null;
        }

        return source.ToDto();
    }
}
