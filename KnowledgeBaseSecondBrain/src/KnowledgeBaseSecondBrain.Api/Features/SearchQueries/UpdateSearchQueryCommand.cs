using KnowledgeBaseSecondBrain.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KnowledgeBaseSecondBrain.Api.Features.SearchQueries;

public record UpdateSearchQueryCommand : IRequest<SearchQueryDto?>
{
    public Guid SearchQueryId { get; init; }
    public string QueryText { get; init; } = string.Empty;
    public string? Name { get; init; }
    public bool IsSaved { get; init; }
}

public class UpdateSearchQueryCommandHandler : IRequestHandler<UpdateSearchQueryCommand, SearchQueryDto?>
{
    private readonly IKnowledgeBaseSecondBrainContext _context;
    private readonly ILogger<UpdateSearchQueryCommandHandler> _logger;

    public UpdateSearchQueryCommandHandler(
        IKnowledgeBaseSecondBrainContext context,
        ILogger<UpdateSearchQueryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SearchQueryDto?> Handle(UpdateSearchQueryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating search query {SearchQueryId}", request.SearchQueryId);

        var searchQuery = await _context.SearchQueries
            .FirstOrDefaultAsync(sq => sq.SearchQueryId == request.SearchQueryId, cancellationToken);

        if (searchQuery == null)
        {
            _logger.LogWarning("Search query {SearchQueryId} not found", request.SearchQueryId);
            return null;
        }

        searchQuery.QueryText = request.QueryText;
        searchQuery.Name = request.Name;
        searchQuery.IsSaved = request.IsSaved;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated search query {SearchQueryId}", request.SearchQueryId);

        return searchQuery.ToDto();
    }
}
