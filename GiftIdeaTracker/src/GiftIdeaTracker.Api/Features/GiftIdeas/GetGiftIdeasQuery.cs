using GiftIdeaTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GiftIdeaTracker.Api.Features.GiftIdeas;

public record GetGiftIdeasQuery : IRequest<IEnumerable<GiftIdeaDto>>
{
    public Guid? UserId { get; init; }
    public Guid? RecipientId { get; init; }
    public Occasion? Occasion { get; init; }
    public bool? IsPurchased { get; init; }
}

public class GetGiftIdeasQueryHandler : IRequestHandler<GetGiftIdeasQuery, IEnumerable<GiftIdeaDto>>
{
    private readonly IGiftIdeaTrackerContext _context;
    private readonly ILogger<GetGiftIdeasQueryHandler> _logger;

    public GetGiftIdeasQueryHandler(
        IGiftIdeaTrackerContext context,
        ILogger<GetGiftIdeasQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<GiftIdeaDto>> Handle(GetGiftIdeasQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting gift ideas for user {UserId}", request.UserId);

        var query = _context.GiftIdeas.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(g => g.UserId == request.UserId.Value);
        }

        if (request.RecipientId.HasValue)
        {
            query = query.Where(g => g.RecipientId == request.RecipientId.Value);
        }

        if (request.Occasion.HasValue)
        {
            query = query.Where(g => g.Occasion == request.Occasion.Value);
        }

        if (request.IsPurchased.HasValue)
        {
            query = query.Where(g => g.IsPurchased == request.IsPurchased.Value);
        }

        var giftIdeas = await query
            .OrderByDescending(g => g.CreatedAt)
            .ToListAsync(cancellationToken);

        return giftIdeas.Select(g => g.ToDto());
    }
}
