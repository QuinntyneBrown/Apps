using Letters.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Letters.Api.Features.Letters;

public record MarkLetterReadCommand : IRequest<LetterDto?>
{
    public Guid LetterId { get; init; }
}

public class MarkLetterReadCommandHandler : IRequestHandler<MarkLetterReadCommand, LetterDto?>
{
    private readonly ILettersDbContext _context;
    private readonly ILogger<MarkLetterReadCommandHandler> _logger;

    public MarkLetterReadCommandHandler(ILettersDbContext context, ILogger<MarkLetterReadCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<LetterDto?> Handle(MarkLetterReadCommand request, CancellationToken cancellationToken)
    {
        var letter = await _context.Letters
            .FirstOrDefaultAsync(l => l.LetterId == request.LetterId, cancellationToken);

        if (letter == null) return null;

        letter.MarkAsRead();
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Letter marked as read: {LetterId}", letter.LetterId);

        return new LetterDto
        {
            LetterId = letter.LetterId,
            UserId = letter.UserId,
            Subject = letter.Subject,
            Content = letter.Content,
            WrittenDate = letter.WrittenDate,
            ScheduledDeliveryDate = letter.ScheduledDeliveryDate,
            ActualDeliveryDate = letter.ActualDeliveryDate,
            DeliveryStatus = letter.DeliveryStatus,
            HasBeenRead = letter.HasBeenRead,
            ReadDate = letter.ReadDate,
            CreatedAt = letter.CreatedAt
        };
    }
}
