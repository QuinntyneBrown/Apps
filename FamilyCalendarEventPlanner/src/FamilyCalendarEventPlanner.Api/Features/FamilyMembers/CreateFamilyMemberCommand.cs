using FamilyCalendarEventPlanner.Core;
using FamilyCalendarEventPlanner.Core.Model.FamilyMemberAggregate;
using FamilyCalendarEventPlanner.Core.Model.FamilyMemberAggregate.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.FamilyMembers;

public record CreateFamilyMemberCommand : IRequest<FamilyMemberDto>
{
    public Guid FamilyId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Email { get; init; }
    public string Color { get; init; } = string.Empty;
    public MemberRole Role { get; init; }
    public bool IsImmediate { get; init; } = true;
    public RelationType RelationType { get; init; } = RelationType.Self;
}

public class CreateFamilyMemberCommandHandler : IRequestHandler<CreateFamilyMemberCommand, FamilyMemberDto>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<CreateFamilyMemberCommandHandler> _logger;

    public CreateFamilyMemberCommandHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<CreateFamilyMemberCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<FamilyMemberDto> Handle(CreateFamilyMemberCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating family member for family {FamilyId}, name: {Name}",
            request.FamilyId,
            request.Name);

        var member = new FamilyMember(
            request.FamilyId,
            request.Name,
            request.Email,
            request.Color,
            request.Role,
            request.IsImmediate,
            request.RelationType);

        _context.FamilyMembers.Add(member);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created family member {MemberId} for family {FamilyId}",
            member.MemberId,
            request.FamilyId);

        return member.ToDto();
    }
}
