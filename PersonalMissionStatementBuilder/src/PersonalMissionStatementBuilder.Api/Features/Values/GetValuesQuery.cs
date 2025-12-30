using PersonalMissionStatementBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalMissionStatementBuilder.Api.Features.Values;

public record GetValuesQuery : IRequest<IEnumerable<ValueDto>>
{
    public Guid? UserId { get; init; }
    public Guid? MissionStatementId { get; init; }
}

public class GetValuesQueryHandler : IRequestHandler<GetValuesQuery, IEnumerable<ValueDto>>
{
    private readonly IPersonalMissionStatementBuilderContext _context;
    private readonly ILogger<GetValuesQueryHandler> _logger;

    public GetValuesQueryHandler(
        IPersonalMissionStatementBuilderContext context,
        ILogger<GetValuesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ValueDto>> Handle(GetValuesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting values for user {UserId}", request.UserId);

        var query = _context.Values.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(v => v.UserId == request.UserId.Value);
        }

        if (request.MissionStatementId.HasValue)
        {
            query = query.Where(v => v.MissionStatementId == request.MissionStatementId.Value);
        }

        var values = await query
            .OrderBy(v => v.Priority)
            .ToListAsync(cancellationToken);

        return values.Select(v => v.ToDto());
    }
}
