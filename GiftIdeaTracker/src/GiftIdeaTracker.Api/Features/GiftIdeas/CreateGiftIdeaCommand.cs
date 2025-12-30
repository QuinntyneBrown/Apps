using GiftIdeaTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GiftIdeaTracker.Api.Features.GiftIdeas;

public record CreateGiftIdeaCommand : IRequest<GiftIdeaDto>
{
    public Guid UserId { get; init; }
    public Guid? RecipientId { get; init; }
    public string Name { get; init; } = string.Empty;
    public Occasion Occasion { get; init; }
    public decimal? EstimatedPrice { get; init; }
}

public class CreateGiftIdeaCommandHandler : IRequestHandler<CreateGiftIdeaCommand, GiftIdeaDto>
{
    private readonly IGiftIdeaTrackerContext _context;
    private readonly ILogger<CreateGiftIdeaCommandHandler> _logger;

    public CreateGiftIdeaCommandHandler(
        IGiftIdeaTrackerContext context,
        ILogger<CreateGiftIdeaCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GiftIdeaDto> Handle(CreateGiftIdeaCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating gift idea for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var giftIdea = new GiftIdea
        {
            GiftIdeaId = Guid.NewGuid(),
            UserId = request.UserId,
            RecipientId = request.RecipientId,
            Name = request.Name,
            Occasion = request.Occasion,
            EstimatedPrice = request.EstimatedPrice,
            IsPurchased = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.GiftIdeas.Add(giftIdea);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created gift idea {GiftIdeaId} for user {UserId}",
            giftIdea.GiftIdeaId,
            request.UserId);

        return giftIdea.ToDto();
    }
}
