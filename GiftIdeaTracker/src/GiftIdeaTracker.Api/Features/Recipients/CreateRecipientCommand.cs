using GiftIdeaTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GiftIdeaTracker.Api.Features.Recipients;

public record CreateRecipientCommand : IRequest<RecipientDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Relationship { get; init; }
}

public class CreateRecipientCommandHandler : IRequestHandler<CreateRecipientCommand, RecipientDto>
{
    private readonly IGiftIdeaTrackerContext _context;
    private readonly ILogger<CreateRecipientCommandHandler> _logger;

    public CreateRecipientCommandHandler(
        IGiftIdeaTrackerContext context,
        ILogger<CreateRecipientCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<RecipientDto> Handle(CreateRecipientCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating recipient for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var recipient = new Recipient
        {
            RecipientId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Relationship = request.Relationship,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Recipients.Add(recipient);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created recipient {RecipientId} for user {UserId}",
            recipient.RecipientId,
            request.UserId);

        return recipient.ToDto();
    }
}
