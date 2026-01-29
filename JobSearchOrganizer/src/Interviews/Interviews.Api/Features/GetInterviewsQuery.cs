using Interviews.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Interviews.Api.Features;

public record GetInterviewsQuery : IRequest<IEnumerable<InterviewDto>>;

public class GetInterviewsQueryHandler : IRequestHandler<GetInterviewsQuery, IEnumerable<InterviewDto>>
{
    private readonly IInterviewsDbContext _context;

    public GetInterviewsQueryHandler(IInterviewsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InterviewDto>> Handle(GetInterviewsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Interviews
            .AsNoTracking()
            .Select(i => i.ToDto())
            .ToListAsync(cancellationToken);
    }
}
