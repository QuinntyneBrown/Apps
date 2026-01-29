using Skills.Core;
using Skills.Core.Models;
using MediatR;

namespace Skills.Api.Features;

public record CreateSkillCommand(Guid TenantId, Guid UserId, string Name, string? Description, int ProficiencyLevel) : IRequest<SkillDto>;

public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, SkillDto>
{
    private readonly ISkillsDbContext _context;

    public CreateSkillCommandHandler(ISkillsDbContext context) => _context = context;

    public async Task<SkillDto> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = new Skill(request.TenantId, request.UserId, request.Name, request.Description, request.ProficiencyLevel);
        _context.Skills.Add(skill);
        await _context.SaveChangesAsync(cancellationToken);
        return new SkillDto(skill.SkillId, skill.TenantId, skill.UserId, skill.Name, skill.Description, skill.ProficiencyLevel, skill.CreatedAt, skill.UpdatedAt);
    }
}
