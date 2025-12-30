using SkillDevelopmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SkillDevelopmentTracker.Api.Features.Skills;

public record GetSkillsQuery : IRequest<IEnumerable<SkillDto>>
{
    public Guid? UserId { get; init; }
    public string? Category { get; init; }
    public ProficiencyLevel? ProficiencyLevel { get; init; }
}

public class GetSkillsQueryHandler : IRequestHandler<GetSkillsQuery, IEnumerable<SkillDto>>
{
    private readonly ISkillDevelopmentTrackerContext _context;
    private readonly ILogger<GetSkillsQueryHandler> _logger;

    public GetSkillsQueryHandler(
        ISkillDevelopmentTrackerContext context,
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

        if (!string.IsNullOrEmpty(request.Category))
        {
            query = query.Where(s => s.Category == request.Category);
        }

        if (request.ProficiencyLevel.HasValue)
        {
            query = query.Where(s => s.ProficiencyLevel == request.ProficiencyLevel.Value);
        }

        var skills = await query
            .OrderBy(s => s.Name)
            .ToListAsync(cancellationToken);

        return skills.Select(s => s.ToDto());
    }
}
