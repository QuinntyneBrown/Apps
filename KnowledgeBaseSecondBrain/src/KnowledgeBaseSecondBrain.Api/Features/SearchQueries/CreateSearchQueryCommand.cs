using KnowledgeBaseSecondBrain.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KnowledgeBaseSecondBrain.Api.Features.SearchQueries;

public record CreateSearchQueryCommand : IRequest<SearchQueryDto>
{
    public Guid UserId { get; init; }
    public string QueryText { get; init; } = string.Empty;
    public string? Name { get; init; }
    public bool IsSaved { get; init; }
}

public class CreateSearchQueryCommandHandler : IRequestHandler<CreateSearchQueryCommand, SearchQueryDto>
{
    private readonly IKnowledgeBaseSecondBrainContext _context;
    private readonly ILogger<CreateSearchQueryCommandHandler> _logger;

    public CreateSearchQueryCommandHandler(
        IKnowledgeBaseSecondBrainContext context,
        ILogger<CreateSearchQueryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SearchQueryDto> Handle(CreateSearchQueryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating search query for user {UserId}, query: {QueryText}",
            request.UserId,
            request.QueryText);

        var searchQuery = new SearchQuery
        {
            SearchQueryId = Guid.NewGuid(),
            UserId = request.UserId,
            QueryText = request.QueryText,
            Name = request.Name,
            IsSaved = request.IsSaved,
            ExecutionCount = 0,
            LastExecutedAt = null,
            CreatedAt = DateTime.UtcNow,
        };

        _context.SearchQueries.Add(searchQuery);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created search query {SearchQueryId} for user {UserId}",
            searchQuery.SearchQueryId,
            request.UserId);

        return searchQuery.ToDto();
    }
}
