using PersonalMissionStatementBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalMissionStatementBuilder.Api.Features.Progresses;

public record GetProgressByIdQuery : IRequest<ProgressDto?>
{
    public Guid ProgressId { get; init; }
}

public class GetProgressByIdQueryHandler : IRequestHandler<GetProgressByIdQuery, ProgressDto?>
{
    private readonly IPersonalMissionStatementBuilderContext _context;
    private readonly ILogger<GetProgressByIdQueryHandler> _logger;

    public GetProgressByIdQueryHandler(
        IPersonalMissionStatementBuilderContext context,
        ILogger<GetProgressByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ProgressDto?> Handle(GetProgressByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting progress {ProgressId}",
            request.ProgressId);

        var progress = await _context.Progresses
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ProgressId == request.ProgressId, cancellationToken);

        return progress?.ToDto();
    }
}
