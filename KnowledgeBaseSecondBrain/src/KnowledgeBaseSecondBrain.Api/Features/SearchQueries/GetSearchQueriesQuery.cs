using KnowledgeBaseSecondBrain.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KnowledgeBaseSecondBrain.Api.Features.SearchQueries;

public record GetSearchQueriesQuery : IRequest<IEnumerable<SearchQueryDto>>
{
    public Guid? UserId { get; init; }
    public bool? IsSaved { get; init; }
}

public class GetSearchQueriesQueryHandler : IRequestHandler<GetSearchQueriesQuery, IEnumerable<SearchQueryDto>>
{
    private readonly IKnowledgeBaseSecondBrainContext _context;
    private readonly ILogger<GetSearchQueriesQueryHandler> _logger;

    public GetSearchQueriesQueryHandler(
        IKnowledgeBaseSecondBrainContext context,
        ILogger<GetSearchQueriesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<SearchQueryDto>> Handle(GetSearchQueriesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting search queries for user {UserId}", request.UserId);

        var query = _context.SearchQueries.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(sq => sq.UserId == request.UserId.Value);
        }

        if (request.IsSaved.HasValue)
        {
            query = query.Where(sq => sq.IsSaved == request.IsSaved.Value);
        }

        var searchQueries = await query
            .OrderByDescending(sq => sq.LastExecutedAt)
            .ToListAsync(cancellationToken);

        return searchQueries.Select(sq => sq.ToDto());
    }
}
