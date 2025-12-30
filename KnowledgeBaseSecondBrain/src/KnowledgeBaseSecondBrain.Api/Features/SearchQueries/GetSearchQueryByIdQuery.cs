using KnowledgeBaseSecondBrain.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KnowledgeBaseSecondBrain.Api.Features.SearchQueries;

public record GetSearchQueryByIdQuery : IRequest<SearchQueryDto?>
{
    public Guid SearchQueryId { get; init; }
}

public class GetSearchQueryByIdQueryHandler : IRequestHandler<GetSearchQueryByIdQuery, SearchQueryDto?>
{
    private readonly IKnowledgeBaseSecondBrainContext _context;
    private readonly ILogger<GetSearchQueryByIdQueryHandler> _logger;

    public GetSearchQueryByIdQueryHandler(
        IKnowledgeBaseSecondBrainContext context,
        ILogger<GetSearchQueryByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SearchQueryDto?> Handle(GetSearchQueryByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting search query {SearchQueryId}", request.SearchQueryId);

        var searchQuery = await _context.SearchQueries
            .AsNoTracking()
            .FirstOrDefaultAsync(sq => sq.SearchQueryId == request.SearchQueryId, cancellationToken);

        if (searchQuery == null)
        {
            _logger.LogWarning("Search query {SearchQueryId} not found", request.SearchQueryId);
            return null;
        }

        return searchQuery.ToDto();
    }
}
