using PersonalWiki.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PersonalWiki.Api.Features.PageRevisions;

public record CreatePageRevisionCommand : IRequest<PageRevisionDto>
{
    public Guid WikiPageId { get; init; }
    public int Version { get; init; }
    public string Content { get; init; } = string.Empty;
    public string? ChangeSummary { get; init; }
    public string? RevisedBy { get; init; }
}

public class CreatePageRevisionCommandHandler : IRequestHandler<CreatePageRevisionCommand, PageRevisionDto>
{
    private readonly IPersonalWikiContext _context;
    private readonly ILogger<CreatePageRevisionCommandHandler> _logger;

    public CreatePageRevisionCommandHandler(
        IPersonalWikiContext context,
        ILogger<CreatePageRevisionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PageRevisionDto> Handle(CreatePageRevisionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating page revision for page {WikiPageId}, version: {Version}",
            request.WikiPageId,
            request.Version);

        var revision = new PageRevision
        {
            PageRevisionId = Guid.NewGuid(),
            WikiPageId = request.WikiPageId,
            Version = request.Version,
            Content = request.Content,
            ChangeSummary = request.ChangeSummary,
            RevisedBy = request.RevisedBy,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Revisions.Add(revision);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created page revision {PageRevisionId} for page {WikiPageId}",
            revision.PageRevisionId,
            request.WikiPageId);

        return revision.ToDto();
    }
}
