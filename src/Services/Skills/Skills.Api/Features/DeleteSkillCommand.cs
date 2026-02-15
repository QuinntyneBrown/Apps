using Skills.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Skills.Api.Features;

public record DeleteSkillCommand(Guid SkillId) : IRequest<bool>;

public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand, bool>
{
    private readonly ISkillsDbContext _context;
    private readonly ILogger<DeleteSkillCommandHandler> _logger;

    public DeleteSkillCommandHandler(ISkillsDbContext context, ILogger<DeleteSkillCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await _context.Skills
            .FirstOrDefaultAsync(s => s.SkillId == request.SkillId, cancellationToken);

        if (skill == null) return false;

        _context.Skills.Remove(skill);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Skill deleted: {SkillId}", request.SkillId);

        return true;
    }
}
