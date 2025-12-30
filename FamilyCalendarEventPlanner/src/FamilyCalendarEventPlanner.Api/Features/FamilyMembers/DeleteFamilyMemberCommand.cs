using FamilyCalendarEventPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.FamilyMembers;

public record DeleteFamilyMemberCommand : IRequest<bool>
{
    public Guid MemberId { get; init; }
}

public class DeleteFamilyMemberCommandHandler : IRequestHandler<DeleteFamilyMemberCommand, bool>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<DeleteFamilyMemberCommandHandler> _logger;

    public DeleteFamilyMemberCommandHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<DeleteFamilyMemberCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteFamilyMemberCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting family member {MemberId}", request.MemberId);

        var member = await _context.FamilyMembers
            .FirstOrDefaultAsync(m => m.MemberId == request.MemberId, cancellationToken);

        if (member == null)
        {
            _logger.LogWarning("Family member {MemberId} not found", request.MemberId);
            return false;
        }

        _context.FamilyMembers.Remove(member);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted family member {MemberId}", request.MemberId);

        return true;
    }
}
