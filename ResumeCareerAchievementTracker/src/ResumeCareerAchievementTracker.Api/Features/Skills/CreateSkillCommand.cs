using ResumeCareerAchievementTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ResumeCareerAchievementTracker.Api.Features.Skills;

public record CreateSkillCommand : IRequest<SkillDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public string ProficiencyLevel { get; init; } = string.Empty;
    public decimal? YearsOfExperience { get; init; }
    public DateTime? LastUsedDate { get; init; }
    public string? Notes { get; init; }
}

public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, SkillDto>
{
    private readonly IResumeCareerAchievementTrackerContext _context;
    private readonly ILogger<CreateSkillCommandHandler> _logger;

    public CreateSkillCommandHandler(
        IResumeCareerAchievementTrackerContext context,
        ILogger<CreateSkillCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SkillDto> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating skill for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var skill = new Skill
        {
            SkillId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Category = request.Category,
            ProficiencyLevel = request.ProficiencyLevel,
            YearsOfExperience = request.YearsOfExperience,
            LastUsedDate = request.LastUsedDate,
            Notes = request.Notes,
            IsFeatured = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Skills.Add(skill);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created skill {SkillId} for user {UserId}",
            skill.SkillId,
            request.UserId);

        return skill.ToDto();
    }
}
