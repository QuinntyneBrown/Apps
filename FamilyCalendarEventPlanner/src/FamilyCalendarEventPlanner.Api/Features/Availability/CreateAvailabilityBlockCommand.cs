using FamilyCalendarEventPlanner.Core;
using FamilyCalendarEventPlanner.Core.Model.FamilyMemberAggregate;
using FamilyCalendarEventPlanner.Core.Model.FamilyMemberAggregate.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.Availability;

public record CreateAvailabilityBlockCommand : IRequest<AvailabilityBlockDto>
{
    public Guid MemberId { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public BlockType BlockType { get; init; }
    public string? Reason { get; init; }
}

public class CreateAvailabilityBlockCommandHandler : IRequestHandler<CreateAvailabilityBlockCommand, AvailabilityBlockDto>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<CreateAvailabilityBlockCommandHandler> _logger;

    public CreateAvailabilityBlockCommandHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<CreateAvailabilityBlockCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<AvailabilityBlockDto> Handle(CreateAvailabilityBlockCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating availability block for member {MemberId}",
            request.MemberId);

        var block = new AvailabilityBlock(
            request.MemberId,
            request.StartTime,
            request.EndTime,
            request.BlockType,
            request.Reason);

        _context.AvailabilityBlocks.Add(block);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created availability block {BlockId} for member {MemberId}",
            block.BlockId,
            request.MemberId);

        return block.ToDto();
    }
}
