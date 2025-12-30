using ProfessionalReadingList.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalReadingList.Api.Features.Notes;

public record GetNotesQuery : IRequest<IEnumerable<NoteDto>>
{
    public Guid? UserId { get; init; }
    public Guid? ResourceId { get; init; }
    public string? Tag { get; init; }
}

public class GetNotesQueryHandler : IRequestHandler<GetNotesQuery, IEnumerable<NoteDto>>
{
    private readonly IProfessionalReadingListContext _context;
    private readonly ILogger<GetNotesQueryHandler> _logger;

    public GetNotesQueryHandler(
        IProfessionalReadingListContext context,
        ILogger<GetNotesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<NoteDto>> Handle(GetNotesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting notes for user {UserId}", request.UserId);

        var query = _context.Notes.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(n => n.UserId == request.UserId.Value);
        }

        if (request.ResourceId.HasValue)
        {
            query = query.Where(n => n.ResourceId == request.ResourceId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Tag))
        {
            query = query.Where(n => n.Tags.Contains(request.Tag));
        }

        var notes = await query
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync(cancellationToken);

        return notes.Select(n => n.ToDto());
    }
}
