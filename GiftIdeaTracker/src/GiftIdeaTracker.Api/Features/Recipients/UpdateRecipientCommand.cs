using GiftIdeaTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GiftIdeaTracker.Api.Features.Recipients;

public record UpdateRecipientCommand : IRequest<RecipientDto?>
{
    public Guid RecipientId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Relationship { get; init; }
}

public class UpdateRecipientCommandHandler : IRequestHandler<UpdateRecipientCommand, RecipientDto?>
{
    private readonly IGiftIdeaTrackerContext _context;
    private readonly ILogger<UpdateRecipientCommandHandler> _logger;

    public UpdateRecipientCommandHandler(
        IGiftIdeaTrackerContext context,
        ILogger<UpdateRecipientCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<RecipientDto?> Handle(UpdateRecipientCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating recipient {RecipientId}", request.RecipientId);

        var recipient = await _context.Recipients
            .FirstOrDefaultAsync(r => r.RecipientId == request.RecipientId, cancellationToken);

        if (recipient == null)
        {
            _logger.LogWarning("Recipient {RecipientId} not found", request.RecipientId);
            return null;
        }

        recipient.Name = request.Name;
        recipient.Relationship = request.Relationship;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated recipient {RecipientId}", request.RecipientId);

        return recipient.ToDto();
    }
}
