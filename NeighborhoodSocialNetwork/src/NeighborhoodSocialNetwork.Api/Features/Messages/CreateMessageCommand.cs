using NeighborhoodSocialNetwork.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace NeighborhoodSocialNetwork.Api.Features.Messages;

public record CreateMessageCommand : IRequest<MessageDto>
{
    public Guid SenderNeighborId { get; init; }
    public Guid RecipientNeighborId { get; init; }
    public string Subject { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
}

public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, MessageDto>
{
    private readonly INeighborhoodSocialNetworkContext _context;
    private readonly ILogger<CreateMessageCommandHandler> _logger;

    public CreateMessageCommandHandler(
        INeighborhoodSocialNetworkContext context,
        ILogger<CreateMessageCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MessageDto> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating message from {SenderNeighborId} to {RecipientNeighborId}",
            request.SenderNeighborId,
            request.RecipientNeighborId);

        var message = new Message
        {
            MessageId = Guid.NewGuid(),
            SenderNeighborId = request.SenderNeighborId,
            RecipientNeighborId = request.RecipientNeighborId,
            Subject = request.Subject,
            Content = request.Content,
            IsRead = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Messages.Add(message);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Created message {MessageId}", message.MessageId);

        return message.ToDto();
    }
}
