using Skills.Core;
using Skills.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Skills.Api.Features;

public record CreateSkillCommand(
    Guid UserId,
    Guid TenantId,
    string Name,
    string Category,
    string ProficiencyLevel,
    decimal? YearsOfExperience,
    bool IsFeatured) : IRequest<SkillDto>;

public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, SkillDto>
{
    private readonly ISkillsDbContext _context;
    private readonly ILogger<CreateSkillCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateSkillCommandHandler(
        ISkillsDbContext context,
        ILogger<CreateSkillCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<SkillDto> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = new Skill
        {
            SkillId = Guid.NewGuid(),
            UserId = request.UserId,
            TenantId = request.TenantId,
            Name = request.Name,
            Category = request.Category,
            ProficiencyLevel = request.ProficiencyLevel,
            YearsOfExperience = request.YearsOfExperience,
            IsFeatured = request.IsFeatured,
            CreatedAt = DateTime.UtcNow
        };

        _context.Skills.Add(skill);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishSkillAddedEventAsync(skill);

        _logger.LogInformation("Skill created: {SkillId}", skill.SkillId);

        return skill.ToDto();
    }

    private Task PublishSkillAddedEventAsync(Skill skill)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("skills-events", ExchangeType.Topic, durable: true);

            var @event = new SkillAddedEvent
            {
                UserId = skill.UserId,
                TenantId = skill.TenantId,
                SkillId = skill.SkillId,
                Name = skill.Name,
                Category = skill.Category,
                ProficiencyLevel = skill.ProficiencyLevel
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("skills-events", "skill.added", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish SkillAddedEvent");
        }

        return Task.CompletedTask;
    }
}
