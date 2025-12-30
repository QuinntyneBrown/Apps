using PersonalWiki.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalWiki.Api.Features.PageLinks;

public record DeletePageLinkCommand : IRequest<bool>
{
    public Guid PageLinkId { get; init; }
}

public class DeletePageLinkCommandHandler : IRequestHandler<DeletePageLinkCommand, bool>
{
    private readonly IPersonalWikiContext _context;
    private readonly ILogger<DeletePageLinkCommandHandler> _logger;

    public DeletePageLinkCommandHandler(
        IPersonalWikiContext context,
        ILogger<DeletePageLinkCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeletePageLinkCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting page link {PageLinkId}", request.PageLinkId);

        var link = await _context.Links
            .FirstOrDefaultAsync(l => l.PageLinkId == request.PageLinkId, cancellationToken);

        if (link == null)
        {
            _logger.LogWarning("Page link {PageLinkId} not found", request.PageLinkId);
            return false;
        }

        _context.Links.Remove(link);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted page link {PageLinkId}", request.PageLinkId);

        return true;
    }
}
