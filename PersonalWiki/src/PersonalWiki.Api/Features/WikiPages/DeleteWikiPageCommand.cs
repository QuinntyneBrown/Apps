using PersonalWiki.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalWiki.Api.Features.WikiPages;

public record DeleteWikiPageCommand : IRequest<bool>
{
    public Guid WikiPageId { get; init; }
}

public class DeleteWikiPageCommandHandler : IRequestHandler<DeleteWikiPageCommand, bool>
{
    private readonly IPersonalWikiContext _context;
    private readonly ILogger<DeleteWikiPageCommandHandler> _logger;

    public DeleteWikiPageCommandHandler(
        IPersonalWikiContext context,
        ILogger<DeleteWikiPageCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteWikiPageCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting wiki page {WikiPageId}", request.WikiPageId);

        var page = await _context.Pages
            .FirstOrDefaultAsync(p => p.WikiPageId == request.WikiPageId, cancellationToken);

        if (page == null)
        {
            _logger.LogWarning("Wiki page {WikiPageId} not found", request.WikiPageId);
            return false;
        }

        _context.Pages.Remove(page);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted wiki page {WikiPageId}", request.WikiPageId);

        return true;
    }
}
