using GiftIdeaTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GiftIdeaTracker.Api.Features.GiftIdeas;

public record GetGiftIdeaByIdQuery : IRequest<GiftIdeaDto?>
{
    public Guid GiftIdeaId { get; init; }
}

public class GetGiftIdeaByIdQueryHandler : IRequestHandler<GetGiftIdeaByIdQuery, GiftIdeaDto?>
{
    private readonly IGiftIdeaTrackerContext _context;
    private readonly ILogger<GetGiftIdeaByIdQueryHandler> _logger;

    public GetGiftIdeaByIdQueryHandler(
        IGiftIdeaTrackerContext context,
        ILogger<GetGiftIdeaByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GiftIdeaDto?> Handle(GetGiftIdeaByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting gift idea {GiftIdeaId}", request.GiftIdeaId);

        var giftIdea = await _context.GiftIdeas
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.GiftIdeaId == request.GiftIdeaId, cancellationToken);

        return giftIdea?.ToDto();
    }
}
