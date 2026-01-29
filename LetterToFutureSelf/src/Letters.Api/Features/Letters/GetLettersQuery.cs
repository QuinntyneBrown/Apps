using Letters.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Letters.Api.Features.Letters;

public record GetLettersQuery : IRequest<IEnumerable<LetterDto>>
{
    public Guid? UserId { get; init; }
}

public class GetLettersQueryHandler : IRequestHandler<GetLettersQuery, IEnumerable<LetterDto>>
{
    private readonly ILettersDbContext _context;

    public GetLettersQueryHandler(ILettersDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LetterDto>> Handle(GetLettersQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Letters.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(l => l.UserId == request.UserId.Value);
        }

        return await query
            .OrderByDescending(l => l.CreatedAt)
            .Select(l => new LetterDto
            {
                LetterId = l.LetterId,
                UserId = l.UserId,
                Subject = l.Subject,
                Content = l.Content,
                WrittenDate = l.WrittenDate,
                ScheduledDeliveryDate = l.ScheduledDeliveryDate,
                ActualDeliveryDate = l.ActualDeliveryDate,
                DeliveryStatus = l.DeliveryStatus,
                HasBeenRead = l.HasBeenRead,
                ReadDate = l.ReadDate,
                CreatedAt = l.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
