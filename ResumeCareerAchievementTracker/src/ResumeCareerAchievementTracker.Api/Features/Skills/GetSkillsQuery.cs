using ResumeCareerAchievementTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ResumeCareerAchievementTracker.Api.Features.Skills;

public record GetSkillsQuery : IRequest<IEnumerable<SkillDto>>
{
    public Guid? UserId { get; init; }
    public string? Category { get; init; }
    public bool? FeaturedOnly { get; init; }
}

public class GetSkillsQueryHandler : IRequestHandler<GetSkillsQuery, IEnumerable<SkillDto>>
{
    private readonly IResumeCareerAchievementTrackerContext _context;
    private readonly ILogger<GetSkillsQueryHandler> _logger;

    public GetSkillsQueryHandler(
        IResumeCareerAchievementTrackerContext context,
        ILogger<GetSkillsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<SkillDto>> Handle(GetSkillsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting skills for user {UserId}", request.UserId);

        var query = _context.Skills.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(s => s.UserId == request.UserId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Category))
        {
            query = query.Where(s => s.Category == request.Category);
        }

        if (request.FeaturedOnly == true)
        {
            query = query.Where(s => s.IsFeatured);
        }

        var skills = await query
            .OrderBy(s => s.Category)
            .ThenBy(s => s.Name)
            .ToListAsync(cancellationToken);

        return skills.Select(s => s.ToDto());
    }
}
