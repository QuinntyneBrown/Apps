using Skills.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Skills.Api.Features;

public record DeleteSkillCommand(Guid SkillId, Guid TenantId) : IRequest<bool>;

public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand, bool>
{
    private readonly ISkillsDbContext _context;

    public DeleteSkillCommandHandler(ISkillsDbContext context) => _context = context;

    public async Task<bool> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await _context.Skills.FirstOrDefaultAsync(s => s.SkillId == request.SkillId && s.TenantId == request.TenantId, cancellationToken);
        if (skill == null) return false;
        _context.Skills.Remove(skill);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
