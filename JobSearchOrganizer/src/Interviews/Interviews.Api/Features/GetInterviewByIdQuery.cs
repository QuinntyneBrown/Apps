using Interviews.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Interviews.Api.Features;

public record GetInterviewByIdQuery(Guid InterviewId) : IRequest<InterviewDto?>;

public class GetInterviewByIdQueryHandler : IRequestHandler<GetInterviewByIdQuery, InterviewDto?>
{
    private readonly IInterviewsDbContext _context;

    public GetInterviewByIdQueryHandler(IInterviewsDbContext context)
    {
        _context = context;
    }

    public async Task<InterviewDto?> Handle(GetInterviewByIdQuery request, CancellationToken cancellationToken)
    {
        var interview = await _context.Interviews
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.InterviewId == request.InterviewId, cancellationToken);

        return interview?.ToDto();
    }
}
