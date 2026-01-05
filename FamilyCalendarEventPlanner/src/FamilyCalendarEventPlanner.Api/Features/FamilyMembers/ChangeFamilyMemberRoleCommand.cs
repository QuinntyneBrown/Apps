using FamilyCalendarEventPlanner.Core;
using FamilyCalendarEventPlanner.Core.Models.FamilyMemberAggregate.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.FamilyMembers;

public record ChangeFamilyMemberRoleCommand : IRequest<FamilyMemberDto?>
{
    public Guid MemberId { get; init; }
    public MemberRole Role { get; init; }
}

public class ChangeFamilyMemberRoleCommandHandler : IRequestHandler<ChangeFamilyMemberRoleCommand, FamilyMemberDto?>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<ChangeFamilyMemberRoleCommandHandler> _logger;

    public ChangeFamilyMemberRoleCommandHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<ChangeFamilyMemberRoleCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<FamilyMemberDto?> Handle(ChangeFamilyMemberRoleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Changing role for family member {MemberId} to {Role}",
            request.MemberId,
            request.Role);

        var member = await _context.FamilyMembers
            .FirstOrDefaultAsync(m => m.MemberId == request.MemberId, cancellationToken);

        if (member == null)
        {
            _logger.LogWarning("Family member {MemberId} not found", request.MemberId);
            return null;
        }

        member.ChangeRole(request.Role);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Changed role for family member {MemberId} to {Role}",
            request.MemberId,
            request.Role);

        return member.ToDto();
    }
}
