using PersonalMissionStatementBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalMissionStatementBuilder.Api.Features.Values;

public record UpdateValueCommand : IRequest<ValueDto>
{
    public Guid ValueId { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public int? Priority { get; init; }
    public Guid? MissionStatementId { get; init; }
}

public class UpdateValueCommandHandler : IRequestHandler<UpdateValueCommand, ValueDto>
{
    private readonly IPersonalMissionStatementBuilderContext _context;
    private readonly ILogger<UpdateValueCommandHandler> _logger;

    public UpdateValueCommandHandler(
        IPersonalMissionStatementBuilderContext context,
        ILogger<UpdateValueCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ValueDto> Handle(UpdateValueCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Updating value {ValueId}",
            request.ValueId);

        var value = await _context.Values
            .FirstOrDefaultAsync(v => v.ValueId == request.ValueId, cancellationToken);

        if (value == null)
        {
            throw new InvalidOperationException($"Value with ID {request.ValueId} not found");
        }

        if (!string.IsNullOrEmpty(request.Name))
        {
            value.Name = request.Name;
        }

        if (request.Description != null)
        {
            value.Description = request.Description;
        }

        if (request.Priority.HasValue)
        {
            value.UpdatePriority(request.Priority.Value);
        }

        if (request.MissionStatementId.HasValue)
        {
            value.MissionStatementId = request.MissionStatementId.Value;
            value.UpdatedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Updated value {ValueId}",
            request.ValueId);

        return value.ToDto();
    }
}
