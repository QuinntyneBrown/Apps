using FamilyCalendarEventPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.FamilyMembers;

public record UpdateFamilyMemberCommand : IRequest<FamilyMemberDto?>
{
    public Guid MemberId { get; init; }
    public string? Name { get; init; }
    public string? Email { get; init; }
    public string? Color { get; init; }
}

public class UpdateFamilyMemberCommandHandler : IRequestHandler<UpdateFamilyMemberCommand, FamilyMemberDto?>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<UpdateFamilyMemberCommandHandler> _logger;

    public UpdateFamilyMemberCommandHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<UpdateFamilyMemberCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<FamilyMemberDto?> Handle(UpdateFamilyMemberCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating family member {MemberId}", request.MemberId);

        var member = await _context.FamilyMembers
            .FirstOrDefaultAsync(m => m.MemberId == request.MemberId, cancellationToken);

        if (member == null)
        {
            _logger.LogWarning("Family member {MemberId} not found", request.MemberId);
            return null;
        }

        member.UpdateProfile(request.Name, request.Email, request.Color);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated family member {MemberId}", request.MemberId);

        return member.ToDto();
    }
}
