using GiftIdeaTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GiftIdeaTracker.Api.Features.Recipients;

public record DeleteRecipientCommand : IRequest<bool>
{
    public Guid RecipientId { get; init; }
}

public class DeleteRecipientCommandHandler : IRequestHandler<DeleteRecipientCommand, bool>
{
    private readonly IGiftIdeaTrackerContext _context;
    private readonly ILogger<DeleteRecipientCommandHandler> _logger;

    public DeleteRecipientCommandHandler(
        IGiftIdeaTrackerContext context,
        ILogger<DeleteRecipientCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteRecipientCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting recipient {RecipientId}", request.RecipientId);

        var recipient = await _context.Recipients
            .FirstOrDefaultAsync(r => r.RecipientId == request.RecipientId, cancellationToken);

        if (recipient == null)
        {
            _logger.LogWarning("Recipient {RecipientId} not found", request.RecipientId);
            return false;
        }

        _context.Recipients.Remove(recipient);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted recipient {RecipientId}", request.RecipientId);

        return true;
    }
}
