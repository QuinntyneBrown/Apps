using FamilyCalendarEventPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.FamilyMembers;

public record GetFamilyMemberByIdQuery : IRequest<FamilyMemberDto?>
{
    public Guid MemberId { get; init; }
}

public class GetFamilyMemberByIdQueryHandler : IRequestHandler<GetFamilyMemberByIdQuery, FamilyMemberDto?>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<GetFamilyMemberByIdQueryHandler> _logger;

    public GetFamilyMemberByIdQueryHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<GetFamilyMemberByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<FamilyMemberDto?> Handle(GetFamilyMemberByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting family member {MemberId}", request.MemberId);

        var member = await _context.FamilyMembers
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MemberId == request.MemberId, cancellationToken);

        return member?.ToDto();
    }
}
