using GiftIdeaTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GiftIdeaTracker.Api.Features.GiftIdeas;

public record UpdateGiftIdeaCommand : IRequest<GiftIdeaDto?>
{
    public Guid GiftIdeaId { get; init; }
    public string Name { get; init; } = string.Empty;
    public Occasion Occasion { get; init; }
    public decimal? EstimatedPrice { get; init; }
    public bool IsPurchased { get; init; }
}

public class UpdateGiftIdeaCommandHandler : IRequestHandler<UpdateGiftIdeaCommand, GiftIdeaDto?>
{
    private readonly IGiftIdeaTrackerContext _context;
    private readonly ILogger<UpdateGiftIdeaCommandHandler> _logger;

    public UpdateGiftIdeaCommandHandler(
        IGiftIdeaTrackerContext context,
        ILogger<UpdateGiftIdeaCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GiftIdeaDto?> Handle(UpdateGiftIdeaCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating gift idea {GiftIdeaId}", request.GiftIdeaId);

        var giftIdea = await _context.GiftIdeas
            .FirstOrDefaultAsync(g => g.GiftIdeaId == request.GiftIdeaId, cancellationToken);

        if (giftIdea == null)
        {
            _logger.LogWarning("Gift idea {GiftIdeaId} not found", request.GiftIdeaId);
            return null;
        }

        giftIdea.Name = request.Name;
        giftIdea.Occasion = request.Occasion;
        giftIdea.EstimatedPrice = request.EstimatedPrice;
        giftIdea.IsPurchased = request.IsPurchased;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated gift idea {GiftIdeaId}", request.GiftIdeaId);

        return giftIdea.ToDto();
    }
}
