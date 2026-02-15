using Lessons.Core;
using Lessons.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Lessons.Api.Features;

public record CreateLessonCommand(
    Guid UserId,
    Guid TenantId,
    Guid? SourceId,
    string Title,
    string Content,
    string Category,
    string? Tags,
    DateTime DateLearned,
    string? Application,
    bool IsApplied) : IRequest<LessonDto>;

public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, LessonDto>
{
    private readonly ILessonsDbContext _context;
    private readonly ILogger<CreateLessonCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateLessonCommandHandler(
        ILessonsDbContext context,
        ILogger<CreateLessonCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<LessonDto> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = new Lesson
        {
            LessonId = Guid.NewGuid(),
            UserId = request.UserId,
            TenantId = request.TenantId,
            SourceId = request.SourceId,
            Title = request.Title,
            Content = request.Content,
            Category = request.Category,
            Tags = request.Tags,
            DateLearned = request.DateLearned,
            Application = request.Application,
            IsApplied = request.IsApplied,
            CreatedAt = DateTime.UtcNow
        };

        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishLessonCreatedEventAsync(lesson);

        _logger.LogInformation("Lesson created: {LessonId}", lesson.LessonId);

        return lesson.ToDto();
    }

    private Task PublishLessonCreatedEventAsync(Lesson lesson)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("lessons-events", ExchangeType.Topic, durable: true);

            var @event = new LessonCreatedEvent
            {
                UserId = lesson.UserId,
                TenantId = lesson.TenantId,
                LessonId = lesson.LessonId,
                Title = lesson.Title,
                Category = lesson.Category,
                SourceId = lesson.SourceId
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("lessons-events", "lesson.created", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish LessonCreatedEvent");
        }

        return Task.CompletedTask;
    }
}
