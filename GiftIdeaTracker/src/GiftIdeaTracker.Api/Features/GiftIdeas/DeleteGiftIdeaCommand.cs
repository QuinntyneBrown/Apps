using GiftIdeaTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GiftIdeaTracker.Api.Features.GiftIdeas;

public record DeleteGiftIdeaCommand : IRequest<bool>
{
    public Guid GiftIdeaId { get; init; }
}

public class DeleteGiftIdeaCommandHandler : IRequestHandler<DeleteGiftIdeaCommand, bool>
{
    private readonly IGiftIdeaTrackerContext _context;
    private readonly ILogger<DeleteGiftIdeaCommandHandler> _logger;

    public DeleteGiftIdeaCommandHandler(
        IGiftIdeaTrackerContext context,
        ILogger<DeleteGiftIdeaCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteGiftIdeaCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting gift idea {GiftIdeaId}", request.GiftIdeaId);

        var giftIdea = await _context.GiftIdeas
            .FirstOrDefaultAsync(g => g.GiftIdeaId == request.GiftIdeaId, cancellationToken);

        if (giftIdea == null)
        {
            _logger.LogWarning("Gift idea {GiftIdeaId} not found", request.GiftIdeaId);
            return false;
        }

        _context.GiftIdeas.Remove(giftIdea);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted gift idea {GiftIdeaId}", request.GiftIdeaId);

        return true;
    }
}
