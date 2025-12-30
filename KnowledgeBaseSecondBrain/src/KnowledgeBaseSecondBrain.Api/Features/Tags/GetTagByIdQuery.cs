using KnowledgeBaseSecondBrain.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KnowledgeBaseSecondBrain.Api.Features.Tags;

public record GetTagByIdQuery : IRequest<TagDto?>
{
    public Guid TagId { get; init; }
}

public class GetTagByIdQueryHandler : IRequestHandler<GetTagByIdQuery, TagDto?>
{
    private readonly IKnowledgeBaseSecondBrainContext _context;
    private readonly ILogger<GetTagByIdQueryHandler> _logger;

    public GetTagByIdQueryHandler(
        IKnowledgeBaseSecondBrainContext context,
        ILogger<GetTagByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TagDto?> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting tag {TagId}", request.TagId);

        var tag = await _context.Tags
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TagId == request.TagId, cancellationToken);

        if (tag == null)
        {
            _logger.LogWarning("Tag {TagId} not found", request.TagId);
            return null;
        }

        return tag.ToDto();
    }
}
