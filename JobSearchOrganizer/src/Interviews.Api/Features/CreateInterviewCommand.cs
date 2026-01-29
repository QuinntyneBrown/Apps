using Interviews.Core;
using Interviews.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Interviews.Api.Features;

public record CreateInterviewCommand(
    Guid UserId,
    Guid TenantId,
    Guid ApplicationId,
    DateTime ScheduledDate,
    InterviewType Type,
    string? InterviewerName,
    string? Location,
    string? MeetingLink,
    string? Notes) : IRequest<InterviewDto>;

public class CreateInterviewCommandHandler : IRequestHandler<CreateInterviewCommand, InterviewDto>
{
    private readonly IInterviewsDbContext _context;
    private readonly ILogger<CreateInterviewCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateInterviewCommandHandler(
        IInterviewsDbContext context,
        ILogger<CreateInterviewCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<InterviewDto> Handle(CreateInterviewCommand request, CancellationToken cancellationToken)
    {
        var interview = new Interview
        {
            InterviewId = Guid.NewGuid(),
            UserId = request.UserId,
            TenantId = request.TenantId,
            ApplicationId = request.ApplicationId,
            ScheduledDate = request.ScheduledDate,
            Type = request.Type,
            InterviewerName = request.InterviewerName,
            Location = request.Location,
            MeetingLink = request.MeetingLink,
            Notes = request.Notes,
            Status = InterviewStatus.Scheduled,
            CreatedAt = DateTime.UtcNow
        };

        _context.Interviews.Add(interview);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishInterviewScheduledEventAsync(interview);

        _logger.LogInformation("Interview created: {InterviewId}", interview.InterviewId);

        return interview.ToDto();
    }

    private Task PublishInterviewScheduledEventAsync(Interview interview)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("interviews-events", ExchangeType.Topic, durable: true);

            var @event = new InterviewScheduledEvent
            {
                UserId = interview.UserId,
                TenantId = interview.TenantId,
                InterviewId = interview.InterviewId,
                ApplicationId = interview.ApplicationId,
                ScheduledDate = interview.ScheduledDate
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("interviews-events", "interview.scheduled", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish InterviewScheduledEvent");
        }

        return Task.CompletedTask;
    }
}
