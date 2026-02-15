using Letters.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Letters.Api.Features.Letters;

public record UpdateLetterCommand : IRequest<LetterDto?>
{
    public Guid LetterId { get; init; }
    public string? Subject { get; init; }
    public string? Content { get; init; }
    public DateTime? ScheduledDeliveryDate { get; init; }
}

public class UpdateLetterCommandHandler : IRequestHandler<UpdateLetterCommand, LetterDto?>
{
    private readonly ILettersDbContext _context;
    private readonly ILogger<UpdateLetterCommandHandler> _logger;

    public UpdateLetterCommandHandler(ILettersDbContext context, ILogger<UpdateLetterCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<LetterDto?> Handle(UpdateLetterCommand request, CancellationToken cancellationToken)
    {
        var letter = await _context.Letters
            .FirstOrDefaultAsync(l => l.LetterId == request.LetterId, cancellationToken);

        if (letter == null) return null;

        if (request.Subject != null) letter.Subject = request.Subject;
        if (request.Content != null) letter.Content = request.Content;
        if (request.ScheduledDeliveryDate.HasValue) letter.ScheduledDeliveryDate = request.ScheduledDeliveryDate.Value;
        letter.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Letter updated: {LetterId}", letter.LetterId);

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
