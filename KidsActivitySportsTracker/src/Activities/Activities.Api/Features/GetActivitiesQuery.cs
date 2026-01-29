using Activities.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Activities.Api.Features;

public record GetActivitiesQuery : IRequest<IEnumerable<ActivityDto>>;

public class GetActivitiesQueryHandler : IRequestHandler<GetActivitiesQuery, IEnumerable<ActivityDto>>
{
    private readonly IActivitiesDbContext _context;

    public GetActivitiesQueryHandler(IActivitiesDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ActivityDto>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Activities
            .AsNoTracking()
            .Select(a => a.ToDto())
            .ToListAsync(cancellationToken);
    }
}
