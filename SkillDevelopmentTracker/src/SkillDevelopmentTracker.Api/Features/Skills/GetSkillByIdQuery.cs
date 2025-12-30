using SkillDevelopmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SkillDevelopmentTracker.Api.Features.Skills;

public record GetSkillByIdQuery : IRequest<SkillDto?>
{
    public Guid SkillId { get; init; }
}

public class GetSkillByIdQueryHandler : IRequestHandler<GetSkillByIdQuery, SkillDto?>
{
    private readonly ISkillDevelopmentTrackerContext _context;
    private readonly ILogger<GetSkillByIdQueryHandler> _logger;

    public GetSkillByIdQueryHandler(
        ISkillDevelopmentTrackerContext context,
        ILogger<GetSkillByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SkillDto?> Handle(GetSkillByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting skill {SkillId}", request.SkillId);

        var skill = await _context.Skills
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.SkillId == request.SkillId, cancellationToken);

        if (skill == null)
        {
            _logger.LogWarning("Skill {SkillId} not found", request.SkillId);
            return null;
        }

        return skill.ToDto();
    }
}
