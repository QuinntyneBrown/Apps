using PersonalWiki.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalWiki.Api.Features.PageLinks;

public record UpdatePageLinkCommand : IRequest<PageLinkDto?>
{
    public Guid PageLinkId { get; init; }
    public Guid SourcePageId { get; init; }
    public Guid TargetPageId { get; init; }
    public string? AnchorText { get; init; }
}

public class UpdatePageLinkCommandHandler : IRequestHandler<UpdatePageLinkCommand, PageLinkDto?>
{
    private readonly IPersonalWikiContext _context;
    private readonly ILogger<UpdatePageLinkCommandHandler> _logger;

    public UpdatePageLinkCommandHandler(
        IPersonalWikiContext context,
        ILogger<UpdatePageLinkCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PageLinkDto?> Handle(UpdatePageLinkCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating page link {PageLinkId}", request.PageLinkId);

        var link = await _context.Links
            .FirstOrDefaultAsync(l => l.PageLinkId == request.PageLinkId, cancellationToken);

        if (link == null)
        {
            _logger.LogWarning("Page link {PageLinkId} not found", request.PageLinkId);
            return null;
        }

        link.SourcePageId = request.SourcePageId;
        link.TargetPageId = request.TargetPageId;
        link.AnchorText = request.AnchorText;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated page link {PageLinkId}", request.PageLinkId);

        return link.ToDto();
    }
}
