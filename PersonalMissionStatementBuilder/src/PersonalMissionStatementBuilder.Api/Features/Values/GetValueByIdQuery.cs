using PersonalMissionStatementBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalMissionStatementBuilder.Api.Features.Values;

public record GetValueByIdQuery : IRequest<ValueDto?>
{
    public Guid ValueId { get; init; }
}

public class GetValueByIdQueryHandler : IRequestHandler<GetValueByIdQuery, ValueDto?>
{
    private readonly IPersonalMissionStatementBuilderContext _context;
    private readonly ILogger<GetValueByIdQueryHandler> _logger;

    public GetValueByIdQueryHandler(
        IPersonalMissionStatementBuilderContext context,
        ILogger<GetValueByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ValueDto?> Handle(GetValueByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting value {ValueId}",
            request.ValueId);

        var value = await _context.Values
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.ValueId == request.ValueId, cancellationToken);

        return value?.ToDto();
    }
}
