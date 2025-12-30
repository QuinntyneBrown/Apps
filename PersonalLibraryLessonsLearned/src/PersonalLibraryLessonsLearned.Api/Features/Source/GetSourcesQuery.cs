using PersonalLibraryLessonsLearned.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalLibraryLessonsLearned.Api.Features.Source;

public record GetSourcesQuery : IRequest<IEnumerable<SourceDto>>
{
    public Guid? UserId { get; init; }
    public string? SourceType { get; init; }
}

public class GetSourcesQueryHandler : IRequestHandler<GetSourcesQuery, IEnumerable<SourceDto>>
{
    private readonly IPersonalLibraryLessonsLearnedContext _context;
    private readonly ILogger<GetSourcesQueryHandler> _logger;

    public GetSourcesQueryHandler(
        IPersonalLibraryLessonsLearnedContext context,
        ILogger<GetSourcesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<SourceDto>> Handle(GetSourcesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting sources for user {UserId}", request.UserId);

        var query = _context.Sources.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(s => s.UserId == request.UserId.Value);
        }

        if (!string.IsNullOrEmpty(request.SourceType))
        {
            query = query.Where(s => s.SourceType == request.SourceType);
        }

        var sources = await query
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync(cancellationToken);

        return sources.Select(s => s.ToDto());
    }
}
