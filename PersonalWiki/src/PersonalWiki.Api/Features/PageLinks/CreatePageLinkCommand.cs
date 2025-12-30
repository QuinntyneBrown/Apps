using PersonalWiki.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PersonalWiki.Api.Features.PageLinks;

public record CreatePageLinkCommand : IRequest<PageLinkDto>
{
    public Guid SourcePageId { get; init; }
    public Guid TargetPageId { get; init; }
    public string? AnchorText { get; init; }
}

public class CreatePageLinkCommandHandler : IRequestHandler<CreatePageLinkCommand, PageLinkDto>
{
    private readonly IPersonalWikiContext _context;
    private readonly ILogger<CreatePageLinkCommandHandler> _logger;

    public CreatePageLinkCommandHandler(
        IPersonalWikiContext context,
        ILogger<CreatePageLinkCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PageLinkDto> Handle(CreatePageLinkCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating page link from {SourcePageId} to {TargetPageId}",
            request.SourcePageId,
            request.TargetPageId);

        var link = new PageLink
        {
            PageLinkId = Guid.NewGuid(),
            SourcePageId = request.SourcePageId,
            TargetPageId = request.TargetPageId,
            AnchorText = request.AnchorText,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Links.Add(link);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created page link {PageLinkId} from {SourcePageId} to {TargetPageId}",
            link.PageLinkId,
            request.SourcePageId,
            request.TargetPageId);

        return link.ToDto();
    }
}
