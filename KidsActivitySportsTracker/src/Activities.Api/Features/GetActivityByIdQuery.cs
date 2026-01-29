using Activities.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Activities.Api.Features;

public record GetActivityByIdQuery(Guid ActivityId) : IRequest<ActivityDto?>;

public class GetActivityByIdQueryHandler : IRequestHandler<GetActivityByIdQuery, ActivityDto?>
{
    private readonly IActivitiesDbContext _context;

    public GetActivityByIdQueryHandler(IActivitiesDbContext context)
    {
        _context = context;
    }

    public async Task<ActivityDto?> Handle(GetActivityByIdQuery request, CancellationToken cancellationToken)
    {
        var activity = await _context.Activities
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.ActivityId == request.ActivityId, cancellationToken);

        return activity?.ToDto();
    }
}
