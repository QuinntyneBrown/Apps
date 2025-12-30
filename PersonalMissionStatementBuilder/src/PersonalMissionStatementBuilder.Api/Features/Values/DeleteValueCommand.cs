using PersonalMissionStatementBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalMissionStatementBuilder.Api.Features.Values;

public record DeleteValueCommand : IRequest<bool>
{
    public Guid ValueId { get; init; }
}

public class DeleteValueCommandHandler : IRequestHandler<DeleteValueCommand, bool>
{
    private readonly IPersonalMissionStatementBuilderContext _context;
    private readonly ILogger<DeleteValueCommandHandler> _logger;

    public DeleteValueCommandHandler(
        IPersonalMissionStatementBuilderContext context,
        ILogger<DeleteValueCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteValueCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Deleting value {ValueId}",
            request.ValueId);

        var value = await _context.Values
            .FirstOrDefaultAsync(v => v.ValueId == request.ValueId, cancellationToken);

        if (value == null)
        {
            _logger.LogWarning(
                "Value {ValueId} not found for deletion",
                request.ValueId);
            return false;
        }

        _context.Values.Remove(value);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Deleted value {ValueId}",
            request.ValueId);

        return true;
    }
}
