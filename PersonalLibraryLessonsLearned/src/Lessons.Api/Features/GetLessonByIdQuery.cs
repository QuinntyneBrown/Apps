using Lessons.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lessons.Api.Features;

public record GetLessonByIdQuery(Guid LessonId) : IRequest<LessonDto?>;

public class GetLessonByIdQueryHandler : IRequestHandler<GetLessonByIdQuery, LessonDto?>
{
    private readonly ILessonsDbContext _context;

    public GetLessonByIdQueryHandler(ILessonsDbContext context)
    {
        _context = context;
    }

    public async Task<LessonDto?> Handle(GetLessonByIdQuery request, CancellationToken cancellationToken)
    {
        var lesson = await _context.Lessons
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.LessonId == request.LessonId, cancellationToken);

        return lesson?.ToDto();
    }
}
