using PersonalLibraryLessonsLearned.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PersonalLibraryLessonsLearned.Api.Features.Source;

public record CreateSourceCommand : IRequest<SourceDto>
{
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Author { get; init; }
    public string SourceType { get; init; } = string.Empty;
    public string? Url { get; init; }
    public DateTime? DateConsumed { get; init; }
    public string? Notes { get; init; }
}

public class CreateSourceCommandHandler : IRequestHandler<CreateSourceCommand, SourceDto>
{
    private readonly IPersonalLibraryLessonsLearnedContext _context;
    private readonly ILogger<CreateSourceCommandHandler> _logger;

    public CreateSourceCommandHandler(
        IPersonalLibraryLessonsLearnedContext context,
        ILogger<CreateSourceCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SourceDto> Handle(CreateSourceCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating source for user {UserId}, title: {Title}",
            request.UserId,
            request.Title);

        var source = new Core.Source
        {
            SourceId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Author = request.Author,
            SourceType = request.SourceType,
            Url = request.Url,
            DateConsumed = request.DateConsumed,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Sources.Add(source);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created source {SourceId} for user {UserId}",
            source.SourceId,
            request.UserId);

        return source.ToDto();
    }
}
