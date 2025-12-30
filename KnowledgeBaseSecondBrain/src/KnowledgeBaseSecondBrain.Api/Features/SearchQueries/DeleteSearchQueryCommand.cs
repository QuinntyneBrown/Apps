using KnowledgeBaseSecondBrain.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KnowledgeBaseSecondBrain.Api.Features.SearchQueries;

public record DeleteSearchQueryCommand : IRequest<bool>
{
    public Guid SearchQueryId { get; init; }
}

public class DeleteSearchQueryCommandHandler : IRequestHandler<DeleteSearchQueryCommand, bool>
{
    private readonly IKnowledgeBaseSecondBrainContext _context;
    private readonly ILogger<DeleteSearchQueryCommandHandler> _logger;

    public DeleteSearchQueryCommandHandler(
        IKnowledgeBaseSecondBrainContext context,
        ILogger<DeleteSearchQueryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteSearchQueryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting search query {SearchQueryId}", request.SearchQueryId);

        var searchQuery = await _context.SearchQueries
            .FirstOrDefaultAsync(sq => sq.SearchQueryId == request.SearchQueryId, cancellationToken);

        if (searchQuery == null)
        {
            _logger.LogWarning("Search query {SearchQueryId} not found", request.SearchQueryId);
            return false;
        }

        _context.SearchQueries.Remove(searchQuery);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted search query {SearchQueryId}", request.SearchQueryId);

        return true;
    }
}
