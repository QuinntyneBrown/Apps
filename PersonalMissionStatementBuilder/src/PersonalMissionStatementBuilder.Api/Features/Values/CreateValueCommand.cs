using PersonalMissionStatementBuilder.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PersonalMissionStatementBuilder.Api.Features.Values;

public record CreateValueCommand : IRequest<ValueDto>
{
    public Guid UserId { get; init; }
    public Guid? MissionStatementId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public int Priority { get; init; }
}

public class CreateValueCommandHandler : IRequestHandler<CreateValueCommand, ValueDto>
{
    private readonly IPersonalMissionStatementBuilderContext _context;
    private readonly ILogger<CreateValueCommandHandler> _logger;

    public CreateValueCommandHandler(
        IPersonalMissionStatementBuilderContext context,
        ILogger<CreateValueCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ValueDto> Handle(CreateValueCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating value for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var value = new Value
        {
            ValueId = Guid.NewGuid(),
            UserId = request.UserId,
            MissionStatementId = request.MissionStatementId,
            Name = request.Name,
            Description = request.Description,
            Priority = request.Priority,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Values.Add(value);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created value {ValueId} for user {UserId}",
            value.ValueId,
            request.UserId);

        return value.ToDto();
    }
}
