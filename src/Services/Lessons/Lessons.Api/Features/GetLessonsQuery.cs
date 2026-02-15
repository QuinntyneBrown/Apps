using Lessons.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lessons.Api.Features;

public record GetLessonsQuery : IRequest<IEnumerable<LessonDto>>;

public class GetLessonsQueryHandler : IRequestHandler<GetLessonsQuery, IEnumerable<LessonDto>>
{
    private readonly ILessonsDbContext _context;

    public GetLessonsQueryHandler(ILessonsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LessonDto>> Handle(GetLessonsQuery request, CancellationToken cancellationToken)
    {
        var lessons = await _context.Lessons
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return lessons.Select(l => l.ToDto());
    }
}
