using Courses.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courses.Api.Features;

public record GetCoursesQuery(Guid TenantId, Guid UserId) : IRequest<IEnumerable<CourseDto>>;

public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, IEnumerable<CourseDto>>
{
    private readonly ICoursesDbContext _context;
    public GetCoursesQueryHandler(ICoursesDbContext context) => _context = context;

    public async Task<IEnumerable<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken) =>
        await _context.Courses.Where(c => c.TenantId == request.TenantId && c.UserId == request.UserId)
            .Select(c => new CourseDto(c.CourseId, c.TenantId, c.UserId, c.Title, c.Provider, c.Url, c.DurationHours, c.IsCompleted, c.CompletedAt, c.CreatedAt))
            .ToListAsync(cancellationToken);
}
