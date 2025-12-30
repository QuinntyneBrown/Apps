using PersonalWiki.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalWiki.Api.Features.PageRevisions;

public record UpdatePageRevisionCommand : IRequest<PageRevisionDto?>
{
    public Guid PageRevisionId { get; init; }
    public string Content { get; init; } = string.Empty;
    public string? ChangeSummary { get; init; }
    public string? RevisedBy { get; init; }
}

public class UpdatePageRevisionCommandHandler : IRequestHandler<UpdatePageRevisionCommand, PageRevisionDto?>
{
    private readonly IPersonalWikiContext _context;
    private readonly ILogger<UpdatePageRevisionCommandHandler> _logger;

    public UpdatePageRevisionCommandHandler(
        IPersonalWikiContext context,
        ILogger<UpdatePageRevisionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PageRevisionDto?> Handle(UpdatePageRevisionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating page revision {PageRevisionId}", request.PageRevisionId);

        var revision = await _context.Revisions
            .FirstOrDefaultAsync(r => r.PageRevisionId == request.PageRevisionId, cancellationToken);

        if (revision == null)
        {
            _logger.LogWarning("Page revision {PageRevisionId} not found", request.PageRevisionId);
            return null;
        }

        revision.Content = request.Content;
        revision.ChangeSummary = request.ChangeSummary;
        revision.RevisedBy = request.RevisedBy;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated page revision {PageRevisionId}", request.PageRevisionId);

        return revision.ToDto();
    }
}
