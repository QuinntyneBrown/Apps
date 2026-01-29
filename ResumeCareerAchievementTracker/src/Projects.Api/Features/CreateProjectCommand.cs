using Projects.Core;
using Projects.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Projects.Api.Features;

public record CreateProjectCommand(
    Guid UserId,
    Guid TenantId,
    string Name,
    string Description,
    string? Organization,
    string? Role,
    DateTime StartDate,
    DateTime? EndDate,
    bool IsFeatured) : IRequest<ProjectDto>;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
{
    private readonly IProjectsDbContext _context;
    private readonly ILogger<CreateProjectCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateProjectCommandHandler(
        IProjectsDbContext context,
        ILogger<CreateProjectCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = request.UserId,
            TenantId = request.TenantId,
            Name = request.Name,
            Description = request.Description,
            Organization = request.Organization,
            Role = request.Role,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            IsFeatured = request.IsFeatured,
            CreatedAt = DateTime.UtcNow
        };

        _context.Projects.Add(project);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishProjectAddedEventAsync(project);

        _logger.LogInformation("Project created: {ProjectId}", project.ProjectId);

        return project.ToDto();
    }

    private Task PublishProjectAddedEventAsync(Project project)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("projects-events", ExchangeType.Topic, durable: true);

            var @event = new ProjectAddedEvent
            {
                UserId = project.UserId,
                TenantId = project.TenantId,
                ProjectId = project.ProjectId,
                Name = project.Name,
                StartDate = project.StartDate
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("projects-events", "project.added", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish ProjectAddedEvent");
        }

        return Task.CompletedTask;
    }
}
