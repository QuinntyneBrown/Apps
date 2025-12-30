using PersonalWiki.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalWiki.Api.Features.WikiPages;

public record UpdateWikiPageCommand : IRequest<WikiPageDto?>
{
    public Guid WikiPageId { get; init; }
    public Guid? CategoryId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Slug { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public PageStatus Status { get; init; }
    public bool IsFeatured { get; init; }
}

public class UpdateWikiPageCommandHandler : IRequestHandler<UpdateWikiPageCommand, WikiPageDto?>
{
    private readonly IPersonalWikiContext _context;
    private readonly ILogger<UpdateWikiPageCommandHandler> _logger;

    public UpdateWikiPageCommandHandler(
        IPersonalWikiContext context,
        ILogger<UpdateWikiPageCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<WikiPageDto?> Handle(UpdateWikiPageCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating wiki page {WikiPageId}", request.WikiPageId);

        var page = await _context.Pages
            .FirstOrDefaultAsync(p => p.WikiPageId == request.WikiPageId, cancellationToken);

        if (page == null)
        {
            _logger.LogWarning("Wiki page {WikiPageId} not found", request.WikiPageId);
            return null;
        }

        page.CategoryId = request.CategoryId;
        page.Title = request.Title;
        page.Slug = request.Slug;
        page.UpdateContent(request.Content);
        page.Status = request.Status;
        page.IsFeatured = request.IsFeatured;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated wiki page {WikiPageId}", request.WikiPageId);

        return page.ToDto();
    }
}
