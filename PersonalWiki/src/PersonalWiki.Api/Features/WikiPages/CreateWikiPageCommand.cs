using PersonalWiki.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PersonalWiki.Api.Features.WikiPages;

public record CreateWikiPageCommand : IRequest<WikiPageDto>
{
    public Guid UserId { get; init; }
    public Guid? CategoryId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Slug { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public PageStatus Status { get; init; }
    public bool IsFeatured { get; init; }
}

public class CreateWikiPageCommandHandler : IRequestHandler<CreateWikiPageCommand, WikiPageDto>
{
    private readonly IPersonalWikiContext _context;
    private readonly ILogger<CreateWikiPageCommandHandler> _logger;

    public CreateWikiPageCommandHandler(
        IPersonalWikiContext context,
        ILogger<CreateWikiPageCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<WikiPageDto> Handle(CreateWikiPageCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating wiki page for user {UserId}, title: {Title}",
            request.UserId,
            request.Title);

        var page = new WikiPage
        {
            WikiPageId = Guid.NewGuid(),
            UserId = request.UserId,
            CategoryId = request.CategoryId,
            Title = request.Title,
            Slug = request.Slug,
            Content = request.Content,
            Status = request.Status,
            Version = 1,
            IsFeatured = request.IsFeatured,
            ViewCount = 0,
            LastModifiedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Pages.Add(page);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created wiki page {WikiPageId} for user {UserId}",
            page.WikiPageId,
            request.UserId);

        return page.ToDto();
    }
}
