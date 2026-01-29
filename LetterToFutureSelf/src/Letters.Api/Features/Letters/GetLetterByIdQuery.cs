using Letters.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Letters.Api.Features.Letters;

public record GetLetterByIdQuery : IRequest<LetterDto?>
{
    public Guid LetterId { get; init; }
}

public class GetLetterByIdQueryHandler : IRequestHandler<GetLetterByIdQuery, LetterDto?>
{
    private readonly ILettersDbContext _context;

    public GetLetterByIdQueryHandler(ILettersDbContext context)
    {
        _context = context;
    }

    public async Task<LetterDto?> Handle(GetLetterByIdQuery request, CancellationToken cancellationToken)
    {
        var letter = await _context.Letters
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.LetterId == request.LetterId, cancellationToken);

        if (letter == null) return null;

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
