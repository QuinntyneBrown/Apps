using DailyJournalingApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DailyJournalingApp.Api.Features.Tags;

public record GetTagsQuery : IRequest<IEnumerable<TagDto>>
{
    public Guid? UserId { get; init; }
}

public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, IEnumerable<TagDto>>
{
    private readonly IDailyJournalingAppContext _context;
    private readonly ILogger<GetTagsQueryHandler> _logger;

    public GetTagsQueryHandler(
        IDailyJournalingAppContext context,
        ILogger<GetTagsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<TagDto>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting tags for user {UserId}", request.UserId);

        var query = _context.Tags.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(t => t.UserId == request.UserId.Value);
        }

        var tags = await query
            .OrderBy(t => t.Name)
            .ToListAsync(cancellationToken);

        return tags.Select(t => t.ToDto());
    }
}
